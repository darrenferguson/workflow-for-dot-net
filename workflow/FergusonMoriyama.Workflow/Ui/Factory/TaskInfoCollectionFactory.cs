using System.Collections.Generic;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;
using FergusonMoriyam.Workflow.Interfaces.Ui.Factory;
using FergusonMoriyam.Workflow.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Factory
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
