using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Adapter
{
    public class TaskInfo : ITaskInfo
    {
        public string Name
        {
            get; set;
        }

        public string Description
        {
            get;
            set;
        }

        public string TypeName
        {
            get;
            set;
        }

        public string AssemblyQualifiedTypeName
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public int Top
        {
            get;
            set;
        }

        public int Left
        {
            get;
            set;
        }

        public bool IsStartTask
        {
            get;
            set;
        }

        public IDictionary<string, object> CustomProperties
        {
            get;
            set;
        }
    }
}
