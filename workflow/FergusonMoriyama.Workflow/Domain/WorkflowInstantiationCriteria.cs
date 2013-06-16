using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Domain
{
    [Serializable]
    public class WorkflowInstantiationCriteria : IWorkflowInstantiationCriteria
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IList<string> Events
        {
            get;
            set;
        }

        public bool CancelEvent
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public int WorkflowConfiguration
        {
            get;
            set;
        }
    }
}
