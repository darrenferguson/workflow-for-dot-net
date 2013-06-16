using System;
using System.Web;
using Bitly;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using umbraco.cms.businesslogic.web;

namespace CustomWorkflowTasks
{
    // Workflow tasks should extend BaseWorkflowTask
    // In theory you could just Implement IWorkflowTask but BaseWorkflowTask initialises some standard properties
    // Workflow tasks *must* be serializable

    [Serializable]
    public class BitLyUrlShorteningTask : BaseWorkflowTask, IRunnableWorkflowTask
    {

        // public properties are exposed to the workflow designer my creating and EntityUi for the task. See: BitLyUrlShorteningTaskEntityUi.cs 
        public string BitLyApiKey { get; set; }
        public string BitLyLogin { get; set; }
        public string DocumentTypeProperty { get; set; }


        public BitLyUrlShorteningTask() : base()
        {
            // A workflow task should let the designer know what possible outcomes
            AvailableTransitions.Add("done");
        }

        // Implement the Run method of IRunnableWorkflowTask
        public void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            // In Umbraco the workflowInstance should always be castable to an UmbracoWorkflowInstance
            var wf = (UmbracoWorkflowInstance) workflowInstance;

            // UmbracoWorkflowInstance has a list of node Ids that are associated with the workflow in the CmsNodes property
            foreach(var nodeId in wf.CmsNodes)
            {
                // We'll assume that only documents are attached to this workflow
                var doc = new Document(nodeId);

                var property = doc.getProperty(DocumentTypeProperty);
                
                if (!String.IsNullOrEmpty((string) property.Value)) continue;

                var host = HttpContext.Current.Request.Url.Host;
                var pageUrl = "http://" + host + umbraco.library.NiceUrl(nodeId);

                var shortUrl = API.Bit(BitLyLogin, BitLyApiKey, pageUrl, "Shorten");

                property.Value = shortUrl;
            }
            
            // The run method of a workflow task is responsible for informing the runtime of the outcome.
            // The outcome should be one of the items in the AvailableTransitions list.
            runtime.Transition(workflowInstance, this, "done");
        }
    }
}
