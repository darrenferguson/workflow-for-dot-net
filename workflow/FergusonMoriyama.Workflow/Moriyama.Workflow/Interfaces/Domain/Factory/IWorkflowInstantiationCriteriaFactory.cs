namespace Moriyama.Workflow.Interfaces.Domain.Factory
{
    public interface IWorkflowInstantiationCriteriaFactory
    {
        IWorkflowInstantiationCriteria Create(string name);
    }
}
