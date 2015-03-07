using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Interfaces.Infrastructure
{
    public interface IConfigurationWorkflowRepository
    {
        IList<IWorkflowConfiguration> Workflows { get; }
        void Persist(IWorkflowConfiguration worfkflow);
        IWorkflowConfiguration RestoreState(IWorkflowConfiguration worfkflow);
    }
}
