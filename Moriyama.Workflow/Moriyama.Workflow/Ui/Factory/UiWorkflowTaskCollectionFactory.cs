using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Interfaces.Ui.Factory;
using Moriyama.Workflow.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Factory
{
    public class UiWorkflowTaskCollectionFactory : IUiWorkflowTaskCollectionFactory
    {
        private static readonly UiWorkflowTaskCollectionFactory Factory = new UiWorkflowTaskCollectionFactory();

        public static UiWorkflowTaskCollectionFactory Instance
        {
            get { return Factory; }
        }

        public IUiWorkflowTaskCollection Create(IEnumerable<IWorkflowTask> tasks, IPointCollection pointCollection)
        {
            var u = new UiWorkflowTaskCollection {UiTasks = new Dictionary<string, IUiWorkflowTask>()};

            foreach(var task in tasks)
            {
                u.UiTasks.Add(task.Id.ToString(), UiWorkflowTaskFactory.Instance.Create(task, pointCollection.Points[task.Id.ToString()]));
            }

            return u;
        }
    }
}
