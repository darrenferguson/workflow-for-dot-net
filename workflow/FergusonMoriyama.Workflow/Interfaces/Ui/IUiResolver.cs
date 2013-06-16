namespace FergusonMoriyam.Workflow.Interfaces.Ui
{
    public interface IUiResolver
    {
        IWorkflowEntityUi Resolve(object typeToFindUiFor);
    }
}
