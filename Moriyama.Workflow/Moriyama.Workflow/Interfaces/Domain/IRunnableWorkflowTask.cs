using Moriyama.Workflow.Interfaces.Application.Runtime;

namespace Moriyama.Workflow.Interfaces.Domain
{
    public interface IRunnableWorkflowTask : IWorkflowTask
    {
        void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime);
    }
}
