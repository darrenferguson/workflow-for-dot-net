using System;
using System.Web;
using Moriyama.Workflow.Umbraco6.Application.Filters;
using umbraco;

namespace Moriyama.Workflow.Umbraco6.Application.Modules
{
    public class RegisterClientResourcesHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PostMapRequestHandler += context_PostMapRequestHandler;
        }

        private void context_PostMapRequestHandler(object sender, EventArgs e)
        {
            var context = sender as HttpApplication;
            var executionPath = context.Request.CurrentExecutionFilePath;
            var umbracoPath = string.Concat(GlobalSettings.Path, "/");
            var defaultAspx = string.Concat(umbracoPath, "default.aspx");
            var umbracoAspx = string.Concat(umbracoPath, "umbraco.aspx");

            if (!executionPath.StartsWith(umbracoPath, StringComparison.CurrentCultureIgnoreCase)) return;

            if (executionPath.Equals(defaultAspx, StringComparison.InvariantCultureIgnoreCase))
            {
                // /umbraco/default.aspx forwards to umbraco.aspx with Server.Transfer, which doesn't work because it doesn't have the right page, i.e. umbraco.aspx
                context.Response.Redirect("umbraco.aspx");
            }
            else if (executionPath.Equals(umbracoAspx, StringComparison.InvariantCultureIgnoreCase))
            {
                context.Response.Filter = new RegisterClientResourcesFilter(context.Response.Filter);
            }
        }
    }
}
