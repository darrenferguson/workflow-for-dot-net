using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Interfaces.Infrastructure
{
    public interface IWorkflowInstanceRepository
    {
        IList<IWorkflowInstance> Workflows { get; }
        void Persist(IWorkflowInstance worfkflow);
        IWorkflowInstance RestoreState(IWorkflowInstance worfkflow);
    }
}
