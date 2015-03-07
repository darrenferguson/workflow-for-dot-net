using System;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using umbraco.BusinessLogic;

namespace Moriyama.Workflow.Umbraco6.Task
{
        [Serializable]
        public class UrlInstantiatorRestartWorkflowTask : BaseDecisionWorkflowTask, IWorkflowTask, IDecisionWorkflowTask
        {
            public string Url { get; set; }

            public UrlInstantiatorRestartWorkflowTask()
                : base()
            {
                AvailableTransitions.Add("restart_workflow");
            }

            public override bool CanTransition(int instantiator)
            {
                var userId = User.GetCurrent().Id;
                return userId == instantiator;
            }
        }
}
