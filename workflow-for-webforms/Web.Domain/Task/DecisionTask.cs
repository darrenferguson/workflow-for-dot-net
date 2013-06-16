using System;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Web.Domain.Task
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
            get { return "Transition.aspx?id={0}"; }
        }
    }
}
