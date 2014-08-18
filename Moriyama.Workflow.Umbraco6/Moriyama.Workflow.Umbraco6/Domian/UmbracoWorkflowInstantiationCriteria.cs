using System;
using System.Collections.Generic;
using Moriyama.Workflow.Domain;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Umbraco6.Domain
{

    [Serializable]
    public class UmbracoWorkflowInstantiationCriteria : WorkflowInstantiationCriteria, IWorkflowInstantiationCriteria
    {
        public string CriteriaOperand { get; set; }

        public bool AllowManualInstantiation { get; set; }

        public IList<int> UserTypes
        {
            get;
            set;
        }

        public IList<int> Users
        {
            get;
            set;
        }

        public IList<int> DocumentTypes
        {
            get;
            set;
        }
    }
}
