using System;
using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Domain
{
    public interface IWorkflowInstance : IWorkflow
    {
        string Comment { get; set; }

        DateTime InstantiationTime { get; set; }

        DateTime StartTime { get; set; }
        DateTime EndTime { get; set;  }

        string TypeName { get; set; }

        bool Started { get; set;  }
        bool Ended { get; set;  }

        
        IWorkflowTask CurrentTask { get; set; }
        IList<IWorkflowTask> Tasks { get; }

        IList<string> TransitionHistory { get; } 
    }
}
