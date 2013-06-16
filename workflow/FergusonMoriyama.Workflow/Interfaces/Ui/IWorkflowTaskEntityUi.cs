using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Ui
{
    public interface IWorkflowTaskEntityUi : IWorkflowEntityUi
    {
        IDictionary<string, string> TransitionDescriptions { get; }
    }
}
