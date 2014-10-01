using System;
using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using umbraco.BusinessLogic;

namespace Moriyama.Workflow.Umbraco6.Task
{
        [Serializable]
        public class UrlGroupDecisionWorkflowTask : BaseDecisionWorkflowTask, IWorkflowTask, IDecisionWorkflowTask
        {
            public IList<int> UserTypes { get; set; }
            public string Url { get; set; }


            public UrlGroupDecisionWorkflowTask()
                : base()
            {
                AvailableTransitions.Add("approve");
                AvailableTransitions.Add("reject");
            }

            public override bool CanTransition()
            {
                if (UserTypes != null)
                {
                    if (UserTypes.Contains(User.GetCurrent().UserType.Id))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
}
