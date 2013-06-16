using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Domain
{
    public interface IWorkflowInstantiationCriteria : IWorkflow
    {
        IList<string> Events { get; set; }
        bool CancelEvent { get; set; }
        bool Active { get; set; }
        int WorkflowConfiguration { get; set; }
    }
}
