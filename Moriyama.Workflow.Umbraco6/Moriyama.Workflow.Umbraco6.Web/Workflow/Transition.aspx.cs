using System;
using System.Reflection;
using System.Web.UI;
using ClientDependency.Core;
using Common.Logging;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Web.Extensions;
using Umbraco.Web.UI.Pages;


[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Util.js", "text/javascript")]
[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Config.js", "text/javascript")]
namespace Moriyama.Workflow.Umbraco6.Web.Workflow
{
    public partial class Transition : UmbracoEnsuredPage
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IWorkflowTaskTransitionService TheTransitionService { get; set; }
        public IGlobalisationService TheGlobalisationService { get; set; }

        private IWorkflowInstance _workflowInstance;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Util.js",
                                              ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Config.js",
                                               ClientDependencyType.Javascript);

            var id = Convert.ToInt32(Request["id"]);
            _workflowInstance = TheTransitionService.GetWorkflowInstance(id);

            CannotTransitionLiteral.Text = TheGlobalisationService.GetString("this_workflow_cannot_be_transitioned");
            TransitionButton.Text = TheGlobalisationService.GetString("transition");

            if (!TheTransitionService.CanTransition(_workflowInstance))
            {
                CannotTransitionLiteral.Visible = true;
                TransitionPanel.Visible = false;
                return;
            }

            TransitionDropDownList.DataSource = TheTransitionService.GetTransitions(_workflowInstance);
            TransitionDropDownList.DataTextField = "Value";
            TransitionDropDownList.DataValueField = "Key";

            TransitionDropDownList.DataBind();
        }

        protected void TransitionButtonClick(object sender, EventArgs e)
        {
            var transiton = TransitionDropDownList.SelectedValue;
            var comment = string.IsNullOrEmpty(TransitionCommentTextBox.Text) ? TheGlobalisationService.GetString("no_comment_supplied") : TransitionCommentTextBox.Text;

            Log.Info(string.Format("Workflow '{0}' '{1}' was transitioned: '{2}'", _workflowInstance.Name, _workflowInstance.Id, comment));

            TheTransitionService.Transition(_workflowInstance, transiton, comment);
            SavedLiteral.Visible = true;
        }
    }
}