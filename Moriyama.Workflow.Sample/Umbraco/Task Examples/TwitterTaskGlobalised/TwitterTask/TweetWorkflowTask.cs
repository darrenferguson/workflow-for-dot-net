using System;
using System.Web;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using Twitterizer;
using umbraco.cms.businesslogic.web;

namespace TwitterTask
{ 
    // Workflow tasks should extend BaseWorkflowTask
    // In theory you could just Implement IWorkflowTask but BaseWorkflowTask initialises some standard properties
    // Workflow tasks *must* be serializable

    [Serializable]
    public class TweetWorkflowTask : BaseWorkflowTask, IRunnableWorkflowTask
    {

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public string TweetText { get; set; }
        public string ShortUrlProperty { get; set; }

        public TweetWorkflowTask()
            : base()
        {
            // A workflow task should let the designer know what possible outcomes
            AvailableTransitions = new[] {"done"};
        }

        // Implement the Run method of IRunnableWorkflowTask
        public void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            // In Umbraco the workflowInstance should always be castable to an UmbracoWorkflowInstance
            var wf = (UmbracoWorkflowInstance)workflowInstance;
            
            var accessToken = new OAuthTokens
            {
                AccessToken = AccessToken,
                AccessTokenSecret = AccessTokenSecret,
                ConsumerKey = ConsumerKey,
                ConsumerSecret = ConsumerSecret
            };

            foreach (var nodeId in wf.CmsNodes)
            {
                // We'll assume that only documents are attached to this workflow
                var doc = new Document(nodeId);

                string pageUrl;

                if(string.IsNullOrEmpty(ShortUrlProperty))
                {
                    // The user hasn't specified a property for a Short URL.
                    var host = HttpContext.Current.Request.Url.Host;
                    pageUrl = "http://" + host + umbraco.library.NiceUrl(nodeId);
                } else
                {
                    pageUrl = (string)doc.getProperty(ShortUrlProperty).Value;
                }

                var tweet = string.Format(TweetText, doc.Text, pageUrl);

                // We could do some error checking with the result here....
                var result = TwitterStatus.Update(accessToken, tweet);
            }

            // The run method of a workflow task is responsible for informing the runtime of the outcome.
            // The outcome should be one of the items in the AvailableTransitions list.
            runtime.Transition(workflowInstance, this, "done");
        }
    }
}
