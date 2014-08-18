using System;
using System.Web.UI;
using ClientDependency.Core;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using Spring.Context.Support;
using FergusonMoriyam.Workflow.Umbraco.Web.Ui.Extensions;
using umbraco.BasePages;

[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js", "text/javascript")]
namespace CustomWorkflowDecisionTask
{
    public partial class CustomTransition : UmbracoEnsuredPage
    {
        protected IWorkflowTaskTransitionService TheTransitionService { get; set; }
        protected IWorkflowInstance WorkflowInstance;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js",
                                              ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js",
                                               ClientDependencyType.Javascript);

            // Get a reference to the application spring context and use it to get a reference
            // to the workflow transition service.  If you know spring.net you could
            // Just add your aspx to the config in ~/config/fmworkflow
            var ctx = ContextRegistry.GetContext();
            TheTransitionService = (IWorkflowTaskTransitionService)ctx.GetObject("TransitionService");

            // Get a workflow instance - id is passed on the request.
            var id = Convert.ToInt32(Request["id"]);
            WorkflowInstance = TheTransitionService.GetWorkflowInstance(id);


            // Check that the workflow is able to be transitioned still - someone may have progressed it in the 
            // meantime.
            CannotTransitionLiteral.Text = "This workflow cannot be transitioned";
            if (!TheTransitionService.CanTransition(WorkflowInstance))
            {
                CannotTransitionLiteral.Visible = true;
                return;
            }

            // Bind a list of possible transitions for current workflow task to the dropdown
            // Transitions are just a dictionary of Key = transition ID, Value = Description
            TransitonDropDownList.DataSource = TheTransitionService.GetTransitions(WorkflowInstance);
            TransitonDropDownList.DataTextField = "Value";
            TransitonDropDownList.DataValueField = "Key";
            TransitonDropDownList.DataBind();
        }

        protected void TransitionButtonClick(object sender, EventArgs e)
        {

            
            // Do some custom stuff.
            var umbracoWorkflowInstance = (UmbracoWorkflowInstance) WorkflowInstance;
            // Stash is a dictionary of objects to pass values between tasks.
            umbracoWorkflowInstance.Stash["Comment"] = "Transitioned at " + DateTime.Now;
            // End of custom stuff.

            // The standard transition stuff here.
            var transiton = TransitonDropDownList.SelectedValue;
            TheTransitionService.Transition(umbracoWorkflowInstance, transiton);

            // Showing this code closes the modal and refreshes the dashboard.
            SavedLiteral.Visible = true;
        }
    }
}