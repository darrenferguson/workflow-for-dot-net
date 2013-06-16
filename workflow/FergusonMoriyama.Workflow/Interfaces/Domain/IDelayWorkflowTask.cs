using System;

namespace FergusonMoriyam.Workflow.Interfaces.Domain
{
    public interface IDelayWorkflowTask : IWorkflowTask
    {
        DateTime StartTime { get; set; }
        bool IsComplete();
    }
}
