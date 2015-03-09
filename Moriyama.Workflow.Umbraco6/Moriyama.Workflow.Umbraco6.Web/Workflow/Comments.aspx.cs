using System;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Umbraco6.Domain;
using umbraco.BasePages;

namespace Moriyama.Workflow.Umbraco6.Web.Workflow
{
    public partial class Comments : UmbracoEnsuredPage
    {
        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }
        public IGlobalisationService TheGlobalisationService { get; set; }

        public UmbracoWorkflowInstance UmbracoWorkflowInstance { get; set; }

        protected override void OnInit(EventArgs e)
        {
            var id = Request["id"];

            UmbracoWorkflowInstance = (UmbracoWorkflowInstance)TheWorkflowInstanceService.GetInstance(Convert.ToInt32(id));

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            
        }
    }
}