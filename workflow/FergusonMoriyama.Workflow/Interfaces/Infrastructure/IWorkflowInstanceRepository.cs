using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Interfaces.Infrastructure
{
    public interface IWorkflowInstanceRepository
    {
        IList<IWorkflowInstance> Workflows { get; }
        void Persist(IWorkflowInstance worfkflow);
        IWorkflowInstance RestoreState(IWorkflowInstance worfkflow);
    }
}
