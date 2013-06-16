using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Interfaces.Application.Runtime
{
    public interface IWorkflowRuntime
    {
        void Start(IWorkflowInstance workflow);

        void Transition(IWorkflowInstance workflow, IWorkflowTask workflowTask, string transitionName);
        void Transition(IWorkflowInstance workflow, IWorkflowTask workflowTask, string transitionName, string comment);

        void RunWorkflows();  
    }
}
