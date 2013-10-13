using System;
using System.Collections.Generic;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.media;

namespace FergusonMoriyam.Workflow.Umbraco.Task
{
    [Serializable]
    public class FilterMediaWorkflowTask : BaseWorkflowTask, IWorkflowTask, IRunnableWorkflowTask
    {
        public IList<int> MediaTypes { get; set; }

        public FilterMediaWorkflowTask()
            : base()
        {
            AvailableTransitions.Add("contains_media");
            AvailableTransitions.Add("does_not_contain_media");
        }

        public void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            // Cast to Umbraco worklow instance.
            var umbracoWorkflowInstance = (UmbracoWorkflowInstance) workflowInstance;

            var count = 0;
            var newCmsNodes = new List<int>();

            foreach(var nodeId in umbracoWorkflowInstance.CmsNodes)
            {
                var n = new CMSNode(nodeId);
                if(!n.IsMedia()) continue;

                var d = new Media(nodeId);
                if (!MediaTypes.Contains(d.ContentType.Id)) continue;
                
                newCmsNodes.Add(nodeId);
                count++;
            }

            umbracoWorkflowInstance.CmsNodes = newCmsNodes;

            var transition = (count > 0) ? "contains_media" : "does_not_contain_media";
            runtime.Transition(workflowInstance, this, transition);
        }
    }
}
