using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Adapter
{
    public class UiWorkflowTaskCollection : IUiWorkflowTaskCollection
    {
        public IDictionary<string, IUiWorkflowTask> UiTasks
        {
            get; set;
        }
    }
}
