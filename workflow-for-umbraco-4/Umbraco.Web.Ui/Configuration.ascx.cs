using System;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientDependency.Core;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Umbraco.Web.Ui.Extensions;
using log4net;

[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Css.Grid.css", "text/css")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js", "text/javascript")]
namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
{

    public partial class Configuration : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IGlobalisationService TheGlobalisationService { get; set; }
        public IWorkflowConfigurationService TheWorkflowConfigurationService { get; set; }
        public Helper TheHelper { get; set; }

        public IWorkflowRuntime TheWorkflowRuntime { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //if (Validator.IsTrial() || Validator.IsInvalid())
            //{
            //    TrialLiteral.Visible = true;
            //    TrialLiteral.Text = string.Format("<p class='trialMode'>{0}</p>", TheGlobalisationService.GetString("trial_mode"));
            //}

            TheWorkflowRuntime.RunWorkflows();

            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Css.Grid.css", ClientDependencyType.Css);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js", ClientDependencyType.Javascript);

            
            CreateButton.Text = TheGlobalisationService.GetString("create_workflow_configuration");

            ((ButtonField)WorkflowConfigGridView.Columns[3]).Text = TheGlobalisationService.GetString("delete");
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            
            base.OnPreRender(e);

            

            WorkflowConfigGridView.DataSource = TheWorkflowConfigurationService.ListConfigurations();
            WorkflowConfigGridView.DataBind();

            if (IsPostBack) return;
                        
            WorkflowConfigsDropDownList.DataSource = TheHelper.WorkflowConfigurationTypes();

            WorkflowConfigsDropDownList.DataTextField = "Name";
            WorkflowConfigsDropDownList.DataValueField = "AssemblyQualifiedName";

            WorkflowConfigsDropDownList.DataBind();
            WorkflowConfigsDropDownList.Visible = WorkflowConfigsDropDownList.Items.Count > 1;
            // EventService.Instance.OnChanged(EventArgs.Empty);
        }

        protected void WorklowConfigsRowDeleting(object sender, GridViewDeleteEventArgs eventArgs)
        {
        }

        protected void WorklowConfigsRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            e.Row.Cells[0].Text = TheGlobalisationService.GetString("description");
            e.Row.Cells[1].Text = TheGlobalisationService.GetString("type");
            e.Row.Cells[2].Text = TheGlobalisationService.GetString("active");
        }

        protected void WorklowConfigsRowCommand(object sender, GridViewCommandEventArgs eventArgs)
        {
            var commandArgument = Convert.ToInt32(eventArgs.CommandArgument);
            var id = (int) ((GridView) sender).DataKeys[commandArgument].Value;

            Log.Info(string.Format("Deleting workflow configuration '{0}'", id));
            TheWorkflowConfigurationService.DeleteWorkflowConfiguration(id);
        }

        protected void CreateButtonClick(object sender, EventArgs e)
        {

            //if (Validator.IsTrial() || Validator.IsInvalid())
            //{
            //    var numConfigs = TheWorkflowConfigurationService.ListConfigurations().Count;
            //    if(numConfigs > 0)
            //    {
            //        throw new Exception(TheGlobalisationService.GetString("only_one_config_in_trial"));
            //    }
            //}

            var workflowName = string.Format("{0} - {1}", TheGlobalisationService.GetString("new_workflow"), DateTime.Now);
            Log.Info(string.Format("Creating new workflow '{0}' of type '{1}'", workflowName, WorkflowConfigsDropDownList.SelectedValue));

            TheWorkflowConfigurationService.CreateWorkflowConfiguration(workflowName, WorkflowConfigsDropDownList.SelectedValue);
        }
    }
}