using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Interfaces.Ui.Factory
{
    public interface IWorkflowTaskUiAdapterFactory
    {
        IWorkflowTaskUiAdapter CreateWorkflowTaskUiAdapter(IWorkflowTask t);
    }
}
