using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Factory
{
    public interface IWorkflowTaskUiAdapterFactory
    {
        IWorkflowTaskUiAdapter CreateWorkflowTaskUiAdapter(IWorkflowTask t);
    }
}
