using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moriyama.Workflow.Interfaces.Domain;
using Common.Logging;

namespace Moriyama.Workflow.Domain
{
    [Serializable]
    public class WorkflowConfiguration :  IWorkflowConfiguration, IWorkflowInstantiator
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsConfigurationActive { get; set; }
        // public string Flags { get; set; }

        public bool IsLocked { get; set; }

        public IWorkflowTask StartTask { get; set; }
        public List<IWorkflowTask> Tasks { get; set; }

        public string TypeName { get; set; }
        
        public virtual IWorkflowInstance CreateInstance()
        {

            return new WorkflowInstance
                       {
                           Name = Name,
                           CurrentTask = StartTask,
                           Tasks = Tasks
                       };

        }

        public void AddTask(IWorkflowTask task)
        {
            Tasks.Add(task);
        }

        public void UpdateTask(IWorkflowTask task)
        {
            var index = Tasks.FindIndex(t => t.Id == task.Id);
            if (index > -1) Tasks[index] = task;
        }

        public void RemoveTask(Guid taskId)
        {
            Tasks.RemoveAll(t => t.Id == taskId);
        }

        public void RemoveTasks()
        {
            Tasks = new List<IWorkflowTask>();
        }

        public void AddTransition(string name, Guid srcTaskId, Guid destTaskId)
        {
            var source = Tasks.Where(task => task.Id == srcTaskId).Single();
            
            if (!source.AvailableTransitions.Contains(name)) return;
                
            if(source.Transitions.ContainsKey(name)) {
                 source.Transitions[name] = destTaskId;
            } else {
                source.Transitions.Add(name, destTaskId);
            }
        }

        public void RemoveTransition(string name, Guid srcTaskId)
        {
             var source = Tasks.Where(task => task.Id == srcTaskId).Single();
             source.Transitions.Remove(name);
        }

        public void RemoveTransitions()
        {
            foreach (var task in Tasks)
            {
                task.Transitions = new Dictionary<string, Guid>();
            }
        }
    }
}
