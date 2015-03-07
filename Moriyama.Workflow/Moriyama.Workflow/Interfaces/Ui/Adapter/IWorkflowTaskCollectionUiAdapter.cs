using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Ui.Adapter
{
    public interface IWorkflowTaskCollectionUiAdapter
    {
        IDictionary<string, IWorkflowTaskUiAdapter> Tasks { get; set; }
    }
}
