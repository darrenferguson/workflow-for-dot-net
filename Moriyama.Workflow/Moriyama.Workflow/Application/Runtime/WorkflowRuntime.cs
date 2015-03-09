using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Moriyama.Workflow.Application.Exception;
using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Infrastructure;
using log4net;

namespace Moriyama.Workflow.Application.Runtime
{
    public class WorkflowRuntime : IWorkflowRuntime, IGlobalisable
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly WorkflowRuntime Runtime = new WorkflowRuntime();
        
        public static WorkflowRuntime Instance
        {
            get { return Runtime; }
        }

        private WorkflowRuntime()
        {
        }
        #endregion

        #region properties
        public IRepository<IWorkflowInstance, int> TheWorkflowInstanceRepository { get; set; }
        public IGlobalisationService TheGlobalisationService { get; set; }

        private void LogAndThrow(System.Exception e)
        {
            Log.Fatal(e.Message, e);
            throw e;
        }
        #endregion

        public void Start(IWorkflowInstance workflowInstance)
        {
            if (workflowInstance.Started)
            {
                LogAndThrow(new WorkflowInstanceAlreadyStartedException(string.Format("Workflow instance '{0}' has already been started", workflowInstance.Id)));
            }

            workflowInstance.Started = true;
            var startTasks = workflowInstance.Tasks.Where(task => task.IsStartTask).ToList();

            if(startTasks.Count == 0)
            {
                LogAndThrow(new WorkflowInstanceHasNoStartTaskException(string.Format("Workflow instance '{0}' does not have a start task", workflowInstance.Id)));
            }

            if(startTasks.Count > 1)
            {
                LogAndThrow(new WorkflowInstanceHasTooManyStartTasksException(string.Format("Workflow instance '{0}' has more than one start task", workflowInstance.Id)));
            }

            workflowInstance.StartTime = DateTime.Now;
            workflowInstance.CurrentTask = startTasks[0];

            if (workflowInstance.CurrentTask is IDelayWorkflowTask)
            {
                ((IDelayWorkflowTask)workflowInstance.CurrentTask).StartTime = DateTime.Now;
            }

            TheWorkflowInstanceRepository.Update(workflowInstance);
        }

        private void EndWorkflow(IWorkflowInstance workflowInstance)
        {
            Log.Info(string.Format("The workflow runtime is ending workflow '{0}' - '{1}'", workflowInstance.Id, workflowInstance.Name));
            Log.Info(string.Join(Environment.NewLine, workflowInstance.TransitionHistory));

            workflowInstance.Ended = true;
            workflowInstance.EndTime = DateTime.Now;
            workflowInstance.CurrentTask = null;  
        }

        public void Transition(IWorkflowInstance workflowInstance, IWorkflowTask workflowTask, string transitionName)
        {
            Log.Info(string.Format("The workflow runtime is transitioning {0} - {1} '{2}' - '{3}'", workflowTask.Name, transitionName, workflowInstance.Id, workflowInstance.Name));
            Transition(workflowInstance, workflowTask, transitionName, "");
        }

        public void Transition(IWorkflowInstance workflowInstance, IWorkflowTask workflowTask, string transitionName, string comment)
        {
            if (workflowTask.Transitions.Values.Count == 0)
            {
                Log.Warn(string.Format("Ending workflow '{0}' as it has no transitions.", workflowInstance.Id));
                EndWorkflow(workflowInstance);
                TheWorkflowInstanceRepository.Update(workflowInstance);
                return;
            }

            if(!workflowTask.Transitions.ContainsKey(transitionName))
            {
                LogAndThrow(new WorkflowTaskInvalidTransitionException(string.Format("Inavlid transition '{0}' for task '{1}'", transitionName, workflowTask.Name)));
            }

            var transitionGuid = workflowTask.Transitions[transitionName];
            var transitionTask = workflowInstance.Tasks.SingleOrDefault(task => task.Id == transitionGuid);

            if(transitionTask == null)
            {
                LogAndThrow(new WorkflowTaskInvalidTransitionException(string.Format("Can't transition to task '{0}' in workflow '{1}' as it is not found", transitionGuid.ToString(), workflowInstance.Id)));
            }

            Log.Info(string.Format("Transitioning workflow '{0}' - '{1}' from '{2}' to '{3}' using transition '{4}'", workflowInstance.Name, workflowInstance.Id, workflowInstance.CurrentTask.Name, transitionTask.Name, transitionName));

            workflowInstance.TransitionHistory.Add(string.Format("{0} - Transition '{1}' - '{2}' -> '{3}' {4}", DateTime.Now, transitionName, workflowInstance.CurrentTask.Name, transitionTask.Name, comment));

            workflowInstance.CurrentTask = transitionTask;

            if (workflowInstance.CurrentTask is IDelayWorkflowTask)
            {
                ((IDelayWorkflowTask)workflowInstance.CurrentTask).StartTime = DateTime.Now;
            }

            TheWorkflowInstanceRepository.Update(workflowInstance);
        }

        public void RunWorkflows()
        {
            Log.Info("Runtime is processing workflows");

            var actionsHappened = true;
            while (actionsHappened)
            {
                actionsHappened = Run();
            }
        }

        private bool Run()
        {
            var workflowInstances = TheWorkflowInstanceRepository.List();
            var actions = 0;

            foreach(var workflowInstance in workflowInstances)
            {
                IWorkflowInstance hydratedInstance;

                try
                {
                    hydratedInstance = TheWorkflowInstanceRepository.RestoreState(workflowInstance);
                }
                catch (System.Exception ex)
                {
                    Log.Warn("hyrdating", ex);
                    continue;
                }

                if(hydratedInstance.Ended)
                {
                    Log.Debug(string.Format("Ending workflow '{0}' and removing from runtime.", hydratedInstance.Id.ToString(CultureInfo.InvariantCulture)));

                    hydratedInstance.Ended = true;
                    

                    // TODO: Should be a config option.
                    TheWorkflowInstanceRepository.Update(hydratedInstance);
                    //TheWorkflowInstanceRepository.Delete(hydratedInstance);
                }
                
                var task = hydratedInstance.CurrentTask;
                if (task == null) continue;
                Log.Debug(string.Format("Checking task '{0}' for runnable", task.Name));

                if (task is IRunnableWorkflowTask)
                {
                    try
                    {
                        ((IRunnableWorkflowTask) task).Run(hydratedInstance, this);
                        actions++;
                    }
                    catch (System.Exception ex)
                    {
                        Log.Error("An error occured during a runnable task (workflow will terminate) - " + task.GetType().FullName, ex);
                        Log.Error(ex.StackTrace);
                        
                        EndWorkflow(hydratedInstance);
                        TheWorkflowInstanceRepository.Update(hydratedInstance);

                        actions++;
                    }
                } else if(task is IDelayWorkflowTask)
                {
                    if(((IDelayWorkflowTask) task).IsComplete())
                    {
                        Log.Debug("Delay task completed");
                        Transition(hydratedInstance, task, task.AvailableTransitions[0]);
                        actions++;
                    }
                }
                else if (task is EndWorkflowTask || typeof(EndWorkflowTask) == task.GetType())
                {
                    Log.Debug(string.Format("Workflow {0} will end as it entered an end task", hydratedInstance.Id));
                    EndWorkflow(hydratedInstance);
                    TheWorkflowInstanceRepository.Update(hydratedInstance);
                    actions++;
                }
            }
            
            return actions > 0;
        }
    }
}
