using System;

namespace Moriyama.Workflow.Interfaces.Domain
{
    public interface IDelayWorkflowTask : IWorkflowTask
    {
        DateTime StartTime { get; set; }
        bool IsComplete();
    }
}
