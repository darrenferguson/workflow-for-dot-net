using System;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using log4net;

namespace Web.Ui
{
    public partial class Criteria : Page
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IGlobalisationService TheGlobalisationService { get; set; }
        public IWorkflowInstantiationCriteriaService TheWorkflowInstantiationCriteriaService { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            CreateCriteriaButton.Text = TheGlobalisationService.GetString("criteria_criteria");
            

        }

        protected void WorklowCriteriaRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            e.Row.Cells[0].Text = TheGlobalisationService.GetString("name");

        }

        protected void CreateCriteriaButtonClick(object sender, EventArgs e)
        {
            var workflowName = TheGlobalisationService.GetString("new_instantiation_criteria");
            Log.Debug(string.Format("Creating new workflow instantiation criteria '{0}'", workflowName));
            TheWorkflowInstantiationCriteriaService.CreateWorkflowInstantiationCriteria(workflowName);
        }

        protected void WorklowCriteriaRowDeleting(object sender, GridViewDeleteEventArgs eventArgs)
        {
        }

        protected void WorklowCriteriaRowCommand(object sender, GridViewCommandEventArgs eventArgs)
        {
            var commandArgument = Convert.ToInt32(eventArgs.CommandArgument);
            var id = (int)((GridView)sender).DataKeys[commandArgument].Value;

            var cmd = eventArgs.CommandName;

            if (cmd.ToLower() != "delete") return;

            Log.Debug(string.Format("Deleting workflow criteria {0}", id));
            TheWorkflowInstantiationCriteriaService.Delete(id);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            CriteriaGridView.DataSource = TheWorkflowInstantiationCriteriaService.List();

            CriteriaGridView.DataBind();

            ((ButtonField)CriteriaGridView.Columns[1]).Text = TheGlobalisationService.GetString("delete");
        }
    }
}