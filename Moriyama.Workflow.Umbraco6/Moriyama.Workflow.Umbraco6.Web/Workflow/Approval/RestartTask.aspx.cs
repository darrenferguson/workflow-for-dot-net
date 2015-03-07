using System;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;

namespace Moriyama.Workflow.Umbraco6.Web.Workflow.Approval
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public IGlobalisationService TheGlobalisationService { get; set; }

        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }

        public IWorkflowRuntime TheWorkflowRuntime { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Request["id"]);

            var instance = TheWorkflowInstanceService.GetInstance(id);

            foreach (var task in instance.Tasks)
            {
                if (task.IsStartTask)
                    instance.CurrentTask = task;
            }

            TheWorkflowInstanceService.Update(instance);
            TheWorkflowRuntime.RunWorkflows();
        }
    }
}