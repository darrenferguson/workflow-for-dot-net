using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using ClientDependency.Core;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Application.Interfaces;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using FergusonMoriyam.Workflow.Umbraco.Web.Ui.Extensions;
using Common.Logging;
using umbraco.BasePages;
using System.Linq;

[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js", "text/javascript")]
namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
{
    public partial class SendToWorkflow : UmbracoEnsuredPage
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IUmbracoWorkflowInstantiationCriteriaValidationService TheCriteriaValidationService { get; set; }
        public IWorkflowInstantiationCriteriaService TheWorkflowInstantiationCriteriaService { get; set; }
        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }
        public IGlobalisationService TheGlobalisationService { get; set; }
        
        public IWorkflowConfigurationService TheWorkflowConfigurationService { get; set; }
        public IWorkflowRuntime TheWorkflowRuntime { get; set; }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            StartWorkflowButton.Text = TheGlobalisationService.GetString("send_to_workflow");

            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Util.js",
                                               ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.Config.js",
                                               ClientDependencyType.Javascript);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            var u = umbraco.BusinessLogic.User.GetCurrent();

            var workflows = new List<IWorkflowConfiguration>();

            foreach(var criteria in TheWorkflowInstantiationCriteriaService.List())
            {
                var hydratedCriteria = TheWorkflowInstantiationCriteriaService.GetCriteria(criteria.Id);

                if (!hydratedCriteria.Active) continue;
                if (!((UmbracoWorkflowInstantiationCriteria)hydratedCriteria).AllowManualInstantiation) continue;
                if (!TheCriteriaValidationService.IsCriteriaValid((UmbracoWorkflowInstantiationCriteria)hydratedCriteria, u)) continue;
                
                workflows.Add(TheWorkflowConfigurationService.GetConfiguration(hydratedCriteria.WorkflowConfiguration));
            }


            if (workflows.Count == 0)
            {
                NoCriteriasLiteral.Text = TheGlobalisationService.GetString("no_valid_workflow_instantiation_criteria");
                NoCriteriasLiteral.Visible = true;
            }
            else
            {
                AvailableCriteriaDropDownList.DataSource = workflows;

                AvailableCriteriaDropDownList.DataTextField = "Name";
                AvailableCriteriaDropDownList.DataValueField = "Id";

                AvailableCriteriaDropDownList.DataBind();
                
                SendToWorkflowPanel.Visible = true;
            }

        }

        protected void StartWorkflowButtonClick(object sender, EventArgs e)
        {
            var ids = Request["ids"].Split(',').ToList().ConvertAll(Convert.ToInt32);

            var workflowConfigId = Convert.ToInt32(AvailableCriteriaDropDownList.SelectedValue);
            var comment = string.IsNullOrEmpty(InstantiationCommentTextBox.Text) ? TheGlobalisationService.GetString("no_comment_supplied") : InstantiationCommentTextBox.Text;

            Log.Info(string.Format("Instantiating workflow {0}: {1}", workflowConfigId, comment));

            var inst = TheWorkflowInstanceService.Instantiate(workflowConfigId, comment);

            foreach(var id in ids)
            {
                ((UmbracoWorkflowInstance) inst).CmsNodes.Add(id);
            }
            
            TheWorkflowInstanceService.Update(inst);
            TheWorkflowInstanceService.Start(inst.Id);

            TheWorkflowRuntime.RunWorkflows();
            SavedLiteral.Visible = true;
        }
    }
}