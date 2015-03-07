using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Ui.Adapter
{
    public interface IUiWorkflowTaskCollection
    {
        IDictionary<string, IUiWorkflowTask> UiTasks { get; }
    }
}
