namespace Moriyama.Workflow.Interfaces.Ui
{
    public interface IUiResolver
    {
        IWorkflowEntityUi Resolve(object typeToFindUiFor);
    }
}
