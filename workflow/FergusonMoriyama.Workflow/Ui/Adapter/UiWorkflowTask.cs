using System.Collections.Generic;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Ui.Adapter;

namespace FergusonMoriyam.Workflow.Ui.Adapter
{
    public class UiWorkflowTask : BaseWorkflowTask, IUiWorkflowTask
    {
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

        public string Class
        {
            get;
            set;
        }

        public IDictionary<string, string> TransitionDescriptions
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
