using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Adapter
{
    public interface IWorkflowTaskUiAdapter
    {
        string Name { get; }
        string TypeName { get; set; }
        string AssemblyQualifiedTypeName { get; set; }
        string Class { get; }
        bool IsStartTask { get; }

        int AvailableTransitions { get; }
        IDictionary<string, string> TransitionDescriptions { get; }
    }
}
