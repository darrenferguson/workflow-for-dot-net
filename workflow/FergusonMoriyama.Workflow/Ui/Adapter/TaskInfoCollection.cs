using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Adapter
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
