namespace Moriyama.Workflow.Interfaces.Domain.Factory
{
    public interface IWorkflowConfigurationFactory
    {
        IWorkflowConfiguration Create(string typeSpec, string name);
    }
}
