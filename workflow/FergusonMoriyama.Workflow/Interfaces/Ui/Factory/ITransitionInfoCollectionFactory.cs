using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Factory
{
    public interface ITransitionInfoCollectionFactory
    {
        ITransitionInfoCollection Parse(string json);
    }
}