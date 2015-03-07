using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Domain.Factory;

namespace Moriyama.Workflow.Umbraco6.Domain.Factory
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
