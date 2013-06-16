using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Factory
{
    public interface ITaskInfoCollectionFactory
    {
        ITaskInfoCollection Parse(string json);
    }
}
