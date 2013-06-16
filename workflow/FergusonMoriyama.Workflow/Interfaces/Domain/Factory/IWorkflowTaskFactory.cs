using System;

namespace FergusonMoriyam.Workflow.Interfaces.Domain.Factory
{
    public interface IWorkflowTaskFactory
    {
        IWorkflowTask CreateTask(string typeDesc);
        IWorkflowTask CreateTask(string name, string typeDesc);
        IWorkflowTask CreateTask(Type t);
    }
}
