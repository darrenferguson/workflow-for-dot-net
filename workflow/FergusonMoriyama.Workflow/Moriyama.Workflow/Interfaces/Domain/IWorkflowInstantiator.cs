namespace Moriyama.Workflow.Interfaces.Domain
{
    public interface IWorkflowInstantiator
    {
        IWorkflowInstance CreateInstance();
    }
}
