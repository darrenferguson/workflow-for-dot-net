using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Interfaces.Ui.Factory
{
    public interface ITransitionInfoCollectionFactory
    {
        ITransitionInfoCollection Parse(string json);
    }
}