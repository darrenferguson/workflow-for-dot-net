using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Ui.Factory;

namespace Moriyama.Workflow.Ui.Adapter
{

    public class WorkflowTaskCollectionUiAdapter : IWorkflowTaskCollectionUiAdapter
    {

        public IDictionary<string, IWorkflowTaskUiAdapter> Tasks { get; set; }

        public WorkflowTaskCollectionUiAdapter(IEnumerable<IWorkflowTask> tasksToAdapt)
        {
            Tasks = new Dictionary<string, IWorkflowTaskUiAdapter>();
            foreach(var task in tasksToAdapt)
            {
                Tasks.Add(task.GetType().FullName, WorkflowTaskUiAdapterFactory.Instance.CreateWorkflowTaskUiAdapter(task));
            }
            
        }
    }
}
