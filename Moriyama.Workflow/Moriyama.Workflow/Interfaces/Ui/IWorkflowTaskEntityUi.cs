using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Ui
{
    public interface IWorkflowTaskEntityUi : IWorkflowEntityUi
    {
        IDictionary<string, string> TransitionDescriptions { get; }
    }
}
