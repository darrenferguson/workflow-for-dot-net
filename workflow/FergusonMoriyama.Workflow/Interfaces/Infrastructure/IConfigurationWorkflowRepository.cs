using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Interfaces.Infrastructure
{
    public interface IConfigurationWorkflowRepository
    {
        IList<IWorkflowConfiguration> Workflows { get; }
        void Persist(IWorkflowConfiguration worfkflow);
        IWorkflowConfiguration RestoreState(IWorkflowConfiguration worfkflow);
    }
}
