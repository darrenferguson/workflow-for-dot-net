using System;
using System.Reflection;
using System.Web.UI;
using ClientDependency.Core;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Web.Ui.Extensions;
using Common.Logging;
using umbraco.BasePages;

[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js", "text/javascript")]
namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
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

            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js",
                                              ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js",
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