using System;
using System.Collections.Generic;
using Moriyama.Workflow.Domain;

namespace Moriyama.Workflow.Umbraco6.Domain
{
    [Serializable]
    public class UmbracoWorkflowInstance : WorkflowInstance
    {
        
        public int Instantiator { get; set; }
        public IList<int> CmsNodes { get; set; }

        public IDictionary<string, object> Stash { get; set; }
    }
}
