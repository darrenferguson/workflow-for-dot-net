using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;
using FergusonMoriyam.Workflow.Interfaces.Ui.Factory;
using FergusonMoriyam.Workflow.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Factory
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
