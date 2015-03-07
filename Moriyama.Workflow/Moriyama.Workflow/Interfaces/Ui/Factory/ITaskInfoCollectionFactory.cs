using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Interfaces.Ui.Factory
{
    public interface ITaskInfoCollectionFactory
    {
        ITaskInfoCollection Parse(string json);
    }
}
