using System;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;

namespace FergusonMoriyam.Workflow.Umbraco.Domain.Task
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
