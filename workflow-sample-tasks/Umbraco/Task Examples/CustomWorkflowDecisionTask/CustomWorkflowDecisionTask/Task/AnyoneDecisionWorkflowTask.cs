using System;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;

namespace CustomWorkflowDecisionTask.Task
{
    [Serializable]
    public class AnyoneDecisionWorkflowTask : BaseDecisionWorkflowTask
    {
        public AnyoneDecisionWorkflowTask() : base()
        {
            AvailableTransitions = new[] {"approve", "reject"};
        }

        public override bool CanTransition()
        {
            return true;
        }
    }
}