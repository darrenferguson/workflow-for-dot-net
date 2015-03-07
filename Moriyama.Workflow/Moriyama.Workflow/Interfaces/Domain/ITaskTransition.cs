using System;

namespace Moriyama.Workflow.Interfaces.Domain
{
    public interface ITaskTransition
    {
        string TransitonName { get; set; }
        Guid To { get; set; }
    }
}
