using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Ui.Adapter
{
    public interface ITaskInfo
    {
        string Name { get; }
        string Description { get; }
        string TypeName { get; }
        string AssemblyQualifiedTypeName { get; }
        string Id { get; }
        int Top { get; }
        int Left { get; }
        bool IsStartTask { get; }
        IDictionary<string, object> CustomProperties { get;  }
    }
}
