using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Interfaces.Ui.Adapter
{
    public interface IUiWorkflowTask : IWorkflowTask
    {
        string TypeName { get; }
        string AssemblyQualifiedTypeName { get; }

        int Top { get; }
        int Left { get; }

        string Class { get; }

        IDictionary<string, string> TransitionDescriptions { get; }
        IDictionary<string, object> CustomProperties { get; }
    }
}
