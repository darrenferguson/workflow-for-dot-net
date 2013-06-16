using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Adapter
{
    public class UiWorkflowTaskCollection : IUiWorkflowTaskCollection
    {
        public IDictionary<string, IUiWorkflowTask> UiTasks
        {
            get; set;
        }
    }
}
