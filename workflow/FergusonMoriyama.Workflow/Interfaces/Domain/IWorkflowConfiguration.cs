using System;
using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Domain
{
    public interface IWorkflowConfiguration : IWorkflow
    {
        bool IsConfigurationActive { get; set; }
        bool IsLocked { get;set; }
        string TypeName { get; set; }

        IWorkflowTask StartTask { get; set; }
        List<IWorkflowTask> Tasks { get; set; }
        
        void AddTask(IWorkflowTask task);
        void UpdateTask(IWorkflowTask task);
        void RemoveTask(Guid taskId);
        void RemoveTasks();

        void AddTransition(string name, Guid srcTaskId, Guid destTaskId);
        void RemoveTransition(string name, Guid srcTaskId);
        void RemoveTransitions();
        
        IWorkflowInstance CreateInstance();
    }
}
