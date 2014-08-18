using System.Collections.Generic;
using Moriyama.Workflow.Application.Reflection;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Interfaces.Ui.Factory;
using Moriyama.Workflow.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Factory
{
    public class TaskInfoCollectionFactory : ITaskInfoCollectionFactory
    {
        private static readonly TaskInfoCollectionFactory Factory = new TaskInfoCollectionFactory();

        public static TaskInfoCollectionFactory Instance
        {
            get { return Factory; }
        }

        public ITaskInfoCollection Parse(string json)
        {
            var t = new TaskInfoCollection {Tasks = new List<ITaskInfo>()};

            var tasks = ((Dictionary<string, object>) Helper.Instance.JsSerializer.DeserializeObject(json)).Values;
            foreach (Dictionary<string, object> task in tasks)
            {
                t.Tasks.Add(TaskInfoFactory.Instance.Create(task));
            }

            return t;
        }
    }
}
