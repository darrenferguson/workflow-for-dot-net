using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Adapter
{
    class TaskInfoCollection : ITaskInfoCollection
    {
        public IList<ITaskInfo> Tasks
        {
            get;
            set;
        }
    }
}
