using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Application;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Interfaces.Application;
using log4net;

namespace Web.Ui
{
    public partial class _Default : Page
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IGlobalisationService TheGlobalisationService { get; set; }
        public IWorkflowConfigurationService TheWorkflowConfigurationService { get; set; }
        public Helper TheHelper { get; set; }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            CreateButton.Text = TheGlobalisationService.GetString("create_workflow_configuration");

            ((ButtonField)WorkflowConfigGridView.Columns[4]).Text = TheGlobalisationService.GetString("delete");

            WorkflowConfigGridView.DataSource = WorkflowConfigurationService.Instance.ListConfigurations();
            WorkflowConfigGridView.DataBind();

            if (IsPostBack) return;

            // Typically you'd inherit from WorkflowConfiguration to create your own config types and use the helper to get th
            // list of options here.
            // WorkflowConfigsDropDownList.DataSource = TheHelper.WorkflowConfigurationTypes();
            WorkflowConfigsDropDownList.DataSource = new List<Type> { typeof(WorkflowConfiguration) };


            WorkflowConfigsDropDownList.DataTextField = "Name";
            WorkflowConfigsDropDownList.DataValueField = "AssemblyQualifiedName";

            WorkflowConfigsDropDownList.DataBind();
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
            e.Row.Cells[3].Text = TheGlobalisationService.GetString("locked");
        }

        protected void WorklowConfigsRowCommand(object sender, GridViewCommandEventArgs eventArgs)
        {

            var commandArgument = Convert.ToInt32(eventArgs.CommandArgument);
            var id = (int) ((GridView) sender).DataKeys[commandArgument].Value;

            Log.Info(string.Format("Deleting workflow configuration '{0}'", id));
            WorkflowConfigurationService.Instance.DeleteWorkflowConfiguration(id);
        }

        protected void CreateButtonClick(object sender, EventArgs e)
        {
            var workflowName = string.Format("{0} - {1}", TheGlobalisationService.GetString("new_workflow"), DateTime.Now);
            Log.Info(string.Format("Creating new workflow '{0}' of type '{1}'", workflowName, WorkflowConfigsDropDownList.SelectedValue));

            WorkflowConfigurationService.Instance.CreateWorkflowConfiguration(workflowName, WorkflowConfigsDropDownList.SelectedValue);
        }
    }
}
