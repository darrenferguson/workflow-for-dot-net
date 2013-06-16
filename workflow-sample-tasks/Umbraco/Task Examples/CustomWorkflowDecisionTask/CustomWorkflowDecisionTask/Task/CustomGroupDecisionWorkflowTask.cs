using System;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;

namespace CustomWorkflowDecisionTask.Task
{
    [Serializable]
    public class CustomGroupDecisionWorkflowTask : GroupDecisionWorkflowTask
    {
        public override string TransitionUrl
        {
            get { return "/umbraco/plugins/fmworkflow/CustomTransition.aspx?id={0}"; }
        }
    }
}