using System;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace Web.Ui
{
    public partial class Transition : System.Web.UI.Page
    {
        public IWorkflowTaskTransitionService TheTransitionService { get; set; }
        private IWorkflowInstance _workflowInstance;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

          
            var id = Convert.ToInt32(Request["id"]);
            _workflowInstance = TheTransitionService.GetWorkflowInstance(id);

           
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

            TheTransitionService.Transition(_workflowInstance, transiton);
            Response.Redirect("Instantiate.aspx");
        }
    }
}