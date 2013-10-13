using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using umbraco.BusinessLogic;

namespace FergusonMoriyam.Workflow.Umbraco.Domain.Task
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
