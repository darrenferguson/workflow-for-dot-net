using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Adapter
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
