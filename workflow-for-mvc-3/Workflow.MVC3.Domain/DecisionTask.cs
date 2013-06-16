using System;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace Workflow.MVC3.Domain
{
    [Serializable]
    public class DecisionTask : BaseWorkflowTask, IDecisionWorkflowTask
    {
        public DecisionTask()
            : base()
        {
            AvailableTransitions.Add("yes");
            AvailableTransitions.Add("no");
        }

        public bool CanTransition()
        {
            // Anyone can transition
            return true;
        }

        public string TransitionUrl
        {
            get { return "/WorkflowTransition/Index/{0}"; }
        }
    }
}
