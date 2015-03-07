using System;
using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Domain;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;

namespace Moriyama.Workflow.Umbraco6.Domain.Task
{
    [Serializable]
    public class PublishDocumentsWorkflowTask : BaseWorkflowTask, IWorkflowTask, IRunnableWorkflowTask
    {
        public PublishDocumentsWorkflowTask()
            : base()
        {
            AvailableTransitions.Add("done");
        }

        public void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            // Cast to Umbraco worklow instance.
            var umbracoWorkflowInstance = (UmbracoWorkflowInstance) workflowInstance;

            foreach(var nodeId in umbracoWorkflowInstance.CmsNodes)
            {
                var n = new CMSNode(nodeId);
                if(!n.IsDocument()) continue;

                var d = new Document(nodeId);
                d.Publish(User.GetUser(0));

                umbraco.library.UpdateDocumentCache(d.Id);
            }

            runtime.Transition(workflowInstance, this, "done");
        }
    }
}
