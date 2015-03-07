using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Interfaces.Ui.Factory
{
    public interface IUiWorkflowTaskFactory
    {
        IUiWorkflowTask Create(IWorkflowTask task, IUiPoint point);
    }
}
