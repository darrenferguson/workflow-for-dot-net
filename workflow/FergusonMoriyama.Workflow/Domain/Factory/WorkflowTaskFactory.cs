using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;

namespace FergusonMoriyam.Workflow.Domain.Factory
{
    public class WorkflowTaskFactory : IWorkflowTaskFactory
    {

        private static readonly WorkflowTaskFactory TaskFactory = new WorkflowTaskFactory();

        public static WorkflowTaskFactory Instance
        {
            get { return TaskFactory; }
        }

        private WorkflowTaskFactory() { }

        public IWorkflowTask CreateTask(Type t)
        {
            return CreateTask(t.AssemblyQualifiedName);
        }

        public IWorkflowTask CreateTask(string typeDesc)
        {
            var workflowTask = (IWorkflowTask)Activator.CreateInstance(Type.GetType(typeDesc));
            
            workflowTask.Id = Guid.NewGuid();
            workflowTask.Transitions = new Dictionary<string, Guid>();

            return workflowTask;
        }

        public IWorkflowTask CreateTask(string name, string typeDesc)
        {
            var workflowTask = CreateTask(typeDesc);
            workflowTask.Name = name;
            return workflowTask;
        }
    }
}
