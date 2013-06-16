using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Interfaces.Ui.Factory
{
    public interface IUiWorkflowTaskCollectionFactory
    {
        IUiWorkflowTaskCollection Create(IEnumerable<IWorkflowTask> tasks, IPointCollection pointCollection);
    }
}
