namespace FergusonMoriyam.Workflow.Interfaces.Domain
{
    public interface IDecisionWorkflowTask
    {
        string TransitionUrl { get;  }
        bool CanTransition();
    }
}
