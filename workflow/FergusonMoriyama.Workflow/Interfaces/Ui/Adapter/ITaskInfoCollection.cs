using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Adapter
{
    public interface ITaskInfoCollection
    {
        IList<ITaskInfo> Tasks { get; }
    }
}
