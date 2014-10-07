using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientDependency.Core;
using Common.Logging;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Web.Extensions;

using Moriyama.Workflow.Umbraco6.Application.Interfaces;
using Moriyama.Workflow.Umbraco6.Domain;
using Umbraco.Web.UI.Pages;

[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Util.js", "text/javascript")]
[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Config.js", "text/javascript")]
namespace Moriyama.Workflow.Umbraco6.Web.Workflow
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

            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Util.js",
                                               ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Config.js",
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
            
            // TODO: custom workflow variables?

            Log.Info(string.Format("Instantiating workflow {0}: {1}", workflowConfigId, comment));

            var inst = TheWorkflowInstanceService.Instantiate(workflowConfigId, comment);

            var flags = new CommaDelimitedStringCollection();

            foreach (ListItem item in FlagsCheckBoxList.Items)
            {
                if (item.Selected)
                    flags.Add(TheGlobalisationService.GetString(item.Text));
            }
            
            inst.Flags = flags.ToString();

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