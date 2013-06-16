using System.Collections.Generic;

namespace Workflow.MVC3.WebUi.Models
{
    public class WorkflowTransitionViewModel
    {
        public int InstanceId { get; set; }
        public bool CanTransition { get; set; }
        public IDictionary<string, string> Transitions { get; set; }
    }
}