using System;
using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using umbraco.BusinessLogic;

namespace Moriyama.Workflow.Umbraco6.Domain.Task
{
    [Serializable]
    public class GroupDecisionWorkflowTask : BaseDecisionWorkflowTask, IWorkflowTask, IDecisionWorkflowTask
    {
        public IList<int> UserTypes { get; set; }
        
        public GroupDecisionWorkflowTask()
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
