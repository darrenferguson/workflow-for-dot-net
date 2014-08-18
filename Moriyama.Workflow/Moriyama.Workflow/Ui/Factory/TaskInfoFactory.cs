using System;
using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Ui.Adapter;
using Moriyama.Workflow.Interfaces.Ui.Factory;
using Moriyama.Workflow.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Factory
{
    public class TaskInfoFactory : ITaskInfoFactory
    {
        private static readonly TaskInfoFactory Factory = new TaskInfoFactory();

        public static TaskInfoFactory Instance
        {
            get { return Factory; }
        }

        public ITaskInfo Create(IDictionary<string, object> task)
        {
            var t = new TaskInfo
                        {
                            Name = (string)task["Name"], 
                            Id = (string) task["Id"], 
                            TypeName = (string) task["TypeName"],
                            AssemblyQualifiedTypeName = (string) task["AssemblyQualifiedTypeName"],
                            Top = Convert.ToInt32(task["Top"]),
                            Left = Convert.ToInt32(task["Left"]),
                            Description = (string)task["Description"],
                            IsStartTask = Convert.ToBoolean(task["IsStartTask"])
                            
                        };
            t.CustomProperties = task.ContainsKey("CustomProperties")
                                     ? (Dictionary<string, object>)task["CustomProperties"]
                                     : new Dictionary<string, object>();


            return t;
        }
    }
}
