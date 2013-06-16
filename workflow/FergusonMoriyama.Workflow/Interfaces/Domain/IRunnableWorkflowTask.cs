using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;

namespace FergusonMoriyam.Workflow.Interfaces.Domain
{
    public interface IRunnableWorkflowTask : IWorkflowTask
    {
        void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime);
    }
}
