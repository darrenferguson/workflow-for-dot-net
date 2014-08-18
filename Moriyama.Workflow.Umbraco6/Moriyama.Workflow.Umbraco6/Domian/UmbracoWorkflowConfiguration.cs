using System;
using System.Collections.Generic;
using Moriyama.Workflow.Domain;
using Moriyama.Workflow.Interfaces.Domain;
using umbraco.BusinessLogic;

namespace Moriyama.Workflow.Umbraco6.Domain
{
    [Serializable]
    public class UmbracoWorkflowConfiguration : WorkflowConfiguration
    {
        public IList<string> InstantiatingEvents { get; set; }
        public bool CancelEvent { get; set; }

        public override IWorkflowInstance CreateInstance()
        {
            return new UmbracoWorkflowInstance
            {
                Name = Name,
                CurrentTask = StartTask,
                Tasks = Tasks,
                CmsNodes = new List<int>(),
                Instantiator = User.GetCurrent().Id,
                Stash = new Dictionary<string, object>()
            };
        }
    }
}
