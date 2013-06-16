using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;

namespace FergusonMoriyam.Workflow.Domain.Factory
{
    public class WorkflowInstantiationCriteriaFactory : IWorkflowInstantiationCriteriaFactory
    {
        private static readonly WorkflowInstantiationCriteriaFactory Factory = new WorkflowInstantiationCriteriaFactory();

        public static WorkflowInstantiationCriteriaFactory Instance
        {
            get { return Factory; }
        }

        private WorkflowInstantiationCriteriaFactory() { }

        public IWorkflowInstantiationCriteria Create(string name)
        {
            var w = new WorkflowInstantiationCriteria

                        {   Name = name, 
                            Events = new List<string>()
                        };

            return w;
        }
    }
}
