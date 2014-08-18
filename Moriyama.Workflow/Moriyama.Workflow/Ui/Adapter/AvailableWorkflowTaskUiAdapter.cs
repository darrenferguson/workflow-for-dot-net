using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Ui.Adapter;

namespace Moriyama.Workflow.Ui.Adapter
{
    public class WorkflowTaskUiAdapter : IWorkflowTaskUiAdapter
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string AssemblyQualifiedTypeName { get; set; }
        public string Class { get; set; }

        public bool IsStartTask
        {
            get;
            set;
        }

        public int AvailableTransitions { get; set; }
        public IDictionary<string, string> TransitionDescriptions { get; set; }
    }
}
