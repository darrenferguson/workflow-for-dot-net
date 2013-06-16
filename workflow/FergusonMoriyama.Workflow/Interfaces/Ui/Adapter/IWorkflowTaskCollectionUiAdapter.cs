using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Adapter
{
    public interface IWorkflowTaskCollectionUiAdapter
    {
        IDictionary<string, IWorkflowTaskUiAdapter> Tasks { get; set; }
    }
}
