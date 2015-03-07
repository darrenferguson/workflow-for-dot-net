using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Interfaces.Ui.Factory
{
    public interface IUiWorkflowTaskCollectionFactory
    {
        IUiWorkflowTaskCollection Create(IEnumerable<IWorkflowTask> tasks, IPointCollection pointCollection);
    }
}
