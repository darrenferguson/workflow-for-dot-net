using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;
using FergusonMoriyam.Workflow.Ui.Factory;

namespace FergusonMoriyam.Workflow.Ui.Adapter
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
