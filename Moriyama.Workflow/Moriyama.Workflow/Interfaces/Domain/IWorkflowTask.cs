using System;
using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Domain
{
    public interface IWorkflowTask
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; }
        bool IsStartTask { get; }
        
        IList<string> AvailableTransitions { get; }
        IDictionary<string, Guid> Transitions { get; set;  }
    }
}
