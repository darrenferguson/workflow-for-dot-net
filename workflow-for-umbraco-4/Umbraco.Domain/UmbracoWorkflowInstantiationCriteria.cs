using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Umbraco.Domain
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
