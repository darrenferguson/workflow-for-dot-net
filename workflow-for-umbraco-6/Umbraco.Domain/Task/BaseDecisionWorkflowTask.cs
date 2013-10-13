using System;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Umbraco.Domain.Task
{
    [Serializable]
    public abstract class BaseDecisionWorkflowTask : BaseWorkflowTask, IWorkflowTask, IDecisionWorkflowTask
    {
        public virtual string TransitionUrl
        {
            get { return "/umbraco/plugins/fmworkflow/Transition.aspx?id={0}"; }
        }

        public virtual bool CanTransition()
        {
            return false; 
        }
    }
}
