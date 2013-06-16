using System;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using log4net;

namespace Web.Ui
{
    public partial class Instantiate : Page
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IGlobalisationService TheGlobalisationService { get; set; }
        
        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }
        public IWorkflowRuntime TheWorkflowRuntime { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            RunWorkflowsButton.Text = TheGlobalisationService.GetString("run_workflows");

            if (Request["id"] == null || IsPostBack) return;

            TheWorkflowInstanceService.Instantiate(Convert.ToInt32(Request["id"]));
            CreatedWorkflowLiteral.Visible = true;

            CreatedWorkflowLiteral.Text = TheGlobalisationService.GetString("create_a_workflow");
        }

        protected void WorklowInstanceRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            e.Row.Cells[0].Text = TheGlobalisationService.GetString("name");
            e.Row.Cells[1].Text = TheGlobalisationService.GetString("instantiation_time");
            e.Row.Cells[2].Text = TheGlobalisationService.GetString("running");
            e.Row.Cells[3].Text = TheGlobalisationService.GetString("current_task");

            
        }

        protected void WorklowInstanceRowDeleting(object sender, GridViewDeleteEventArgs eventArgs)
        {
        }

        protected void WorklowInstanceRowCommand(object sender, GridViewCommandEventArgs eventArgs)
        {
            var commandArgument = Convert.ToInt32(eventArgs.CommandArgument);
            var id = (int)((GridView)sender).DataKeys[commandArgument].Value;

            var cmd = eventArgs.CommandName;

            switch (cmd.ToLower())
            {
                case "delete":
                    TheWorkflowInstanceService.DeleteWorkflowInstance(id);
                    break;
                case "start":
                    TheWorkflowInstanceService.Start(id);
                    TheWorkflowRuntime.RunWorkflows();
                    break;
            }
        }

        protected override void  OnPreRender(EventArgs e)
        {
 	        base.OnPreRender(e);

            WorkflowInstancesGridView.DataSource = TheWorkflowInstanceService.ListInstances();
            WorkflowInstancesGridView.DataBind();
        }

        protected void RunWorkflowsButtonClick(object sender, EventArgs e)
        {
            TheWorkflowRuntime.RunWorkflows();
        }


        protected bool CanTransition(IWorkflowTask t)
        {
            if (!typeof(IDecisionWorkflowTask).IsAssignableFrom(t.GetType())) return false;
            return ((IDecisionWorkflowTask)t).CanTransition();
        }

        public string TransitionInfo(IWorkflowInstance i)
        {
            if (i.CurrentTask == null) return "";
            if (!typeof(IDecisionWorkflowTask).IsAssignableFrom(i.CurrentTask.GetType())) return "";

            if (!CanTransition(i.CurrentTask))
            {
                return "";
            }

            var decision = (IDecisionWorkflowTask)i.CurrentTask;
            return "<a href='" + string.Format(decision.TransitionUrl, HttpUtility.UrlEncode(i.Id.ToString())) + "'>" + TheGlobalisationService.GetString("transtion") + "</a>";
        }
    }
}