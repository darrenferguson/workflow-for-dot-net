using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Ui.Adapter
{
    public interface ITaskInfoCollection
    {
        IList<ITaskInfo> Tasks { get; }
    }
}
