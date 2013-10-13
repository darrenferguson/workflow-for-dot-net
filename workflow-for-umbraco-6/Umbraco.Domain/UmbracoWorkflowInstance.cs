using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Domain;

namespace FergusonMoriyam.Workflow.Umbraco.Domain
{
    [Serializable]
    public class UmbracoWorkflowInstance : WorkflowInstance
    {
        
        public int Instantiator { get; set; }
        public IList<int> CmsNodes { get; set; }

        public IDictionary<string, object> Stash { get; set; }
    }
}
