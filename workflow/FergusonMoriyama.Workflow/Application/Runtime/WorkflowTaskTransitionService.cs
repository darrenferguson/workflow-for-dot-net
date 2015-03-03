using System;
using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using log4net;

namespace FergusonMoriyam.Workflow.Application.Runtime
{
    public class WorkflowTaskTransitionService : IWorkflowTaskTransitionService
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singelton
        private static readonly WorkflowTaskTransitionService Service = new WorkflowTaskTransitionService();

        public static WorkflowTaskTransitionService Instance
        {
            get { return Service; }
        }

        private WorkflowTaskTransitionService()
        {
        }
        #endregion

        #region properties
        public IGlobalisationService TheGlobalisationService { get; set; }
        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }
        public IWorkflowRuntime TheWorkflowRuntime { get; set; }
        public IUiResolver TheWorkflowEntityUiResolver { get; set; }
        #endregion

        public IWorkflowInstance GetWorkflowInstance(int workflowInstanceId)
        {
            return TheWorkflowInstanceService.GetInstance(workflowInstanceId);
        }

        public IDictionary<string, string> GetTransitions(IWorkflowInstance workflowInstance)
        {
            var ui = (IWorkflowTaskEntityUi)TheWorkflowEntityUiResolver.Resolve(workflowInstance.CurrentTask);
            var transitons = new Dictionary<string, string>();
            foreach (var transition in workflowInstance.CurrentTask.Transitions)
            {
                transitons.Add(transition.Key, ui.TransitionDescriptions[transition.Key]); 
            }
            return transitons;
        }

        public void Transition(IWorkflowInstance workflowInstance, string transiton)
        {
            Transition(workflowInstance, transiton, "");
        }

        public void Transition(IWorkflowInstance workflowInstance, string transiton, string comment)
        {
            var task = workflowInstance.CurrentTask;

            if (!task.Transitions.ContainsKey(transiton))
            {
                Log.Warn(string.Format("Transition '{0}' not found for task '{1}' - '{2}'", transiton, task.Name, task.Id));
                return;
            }

            Log.Info(string.Format("Performing transition '{0}' for task '{1}' - '{2}'", transiton, task.Name, task.Id));
            TheWorkflowRuntime.Transition(workflowInstance, task, transiton, comment);
            TheWorkflowRuntime.RunWorkflows();
        }

        public bool CanTransition(IWorkflowInstance workflowInstance)
        {
            if (workflowInstance.CurrentTask == null)
            {
                return false;
            }

            var task = workflowInstance.CurrentTask;
            return typeof(IDecisionWorkflowTask).IsAssignableFrom(task.GetType());
        }
    }
}
