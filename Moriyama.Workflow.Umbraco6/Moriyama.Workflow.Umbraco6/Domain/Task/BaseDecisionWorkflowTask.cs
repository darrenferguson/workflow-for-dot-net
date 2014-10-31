using System;
using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Umbraco6.Domain.Task
{
    [Serializable]
    public abstract class BaseDecisionWorkflowTask : BaseWorkflowTask, IWorkflowTask, IDecisionWorkflowTask
    {
        public virtual string TransitionUrl
        {
            get { return "/Workflow/Transition.aspx?id={0}"; }
        }

        public virtual bool CanTransition(int instantiator)
        {
            return false; 
        }
    }
}
