using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;

namespace FergusonMoriyam.Workflow.Umbraco.Domain.Factory
{
    public class UmbracoWorkflowInstantiationCriteriaFactory : IWorkflowInstantiationCriteriaFactory
    {
        private static readonly UmbracoWorkflowInstantiationCriteriaFactory Factory = new UmbracoWorkflowInstantiationCriteriaFactory();

        public static UmbracoWorkflowInstantiationCriteriaFactory Instance
        {
            get { return Factory; }
        }

        private UmbracoWorkflowInstantiationCriteriaFactory() { }

        public IWorkflowInstantiationCriteria Create(string name)
        {
            var w = new UmbracoWorkflowInstantiationCriteria

                        {   Name = name, 
                            Events = new List<string>()
                        };

            return w;
        }
    }
}
