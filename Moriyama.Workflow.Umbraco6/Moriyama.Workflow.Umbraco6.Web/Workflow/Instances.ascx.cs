using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientDependency.Core;
using Common.Logging;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Task;
using Moriyama.Workflow.Umbraco6.Web.Extensions;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using Moriyama.Workflow.Umbraco6.Domain;
using umbraco.cms.helpers;

[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Css.Grid.css", "text/css")]
[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Util.js", "text/javascript")]
[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Config.js", "text/javascript")]
namespace Moriyama.Workflow.Umbraco6.Web.Workflow
{

    public partial class Instances : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IGlobalisationService TheGlobalisationService { get; set; }
        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }

        public IWorkflowRuntime TheWorkflowRuntime { get; set; }


        private IList<UmbracoWorkflowInstance> _hydratedInstances;
        private User _currentUser;
        private bool _isAdmin;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //if (Validator.IsTrial() || Validator.IsInvalid())
            //{
            //    TrialLiteral.Visible = true;
            //    TrialLiteral.Text = string.Format("<p class='trialMode'>{0}</p>", TheGlobalisationService.GetString("trial_mode"));
            //}

            TheWorkflowRuntime.RunWorkflows();

            _currentUser = User.GetCurrent();
            _isAdmin = _currentUser.UserType.Alias.ToLower() == "admin";

            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Css.Grid.css", ClientDependencyType.Css);
            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Util.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Config.js", ClientDependencyType.Javascript);

            ((ButtonField)WorkflowInstancesGridView.Columns[7]).Text = TheGlobalisationService.GetString("delete");
        }

        protected void WorklowInstanceRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            e.Row.Cells[0].Text = TheGlobalisationService.GetString("name");
            e.Row.Cells[1].Text = TheGlobalisationService.GetString("instantiation_time");
            e.Row.Cells[2].Text = TheGlobalisationService.GetString("instantiator"); 
            e.Row.Cells[3].Text = TheGlobalisationService.GetString("running");
            e.Row.Cells[4].Text = TheGlobalisationService.GetString("current_task");
            e.Row.Cells[5].Text = TheGlobalisationService.GetString("transition");
            e.Row.Cells[6].Text = TheGlobalisationService.GetString("attachments");
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
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            _hydratedInstances = new List<UmbracoWorkflowInstance>();
            foreach (var instance in TheWorkflowInstanceService.ListInstances())
            {
                _hydratedInstances.Add((UmbracoWorkflowInstance)TheWorkflowInstanceService.GetInstance(instance.Id));
            }

           
            if (_isAdmin)
            {
                WorkflowInstancesGridView.DataSource = _hydratedInstances;
                WorkflowInstancesGridView.DataBind();
            } else
            {
                
                var filtered = _hydratedInstances.Where(i => i.Instantiator == _currentUser.Id || CanTransition(i.CurrentTask, i.Instantiator)).ToList();
                WorkflowInstancesGridView.DataSource = filtered;

                WorkflowInstancesGridView.DataBind();
                if (filtered.Count > 0)
                {
                    WorkflowInstancesGridView.Columns[7].Visible = false;
                }
            }
            
        }

        protected bool CanTransition(IWorkflowTask t, int instantiator)
        {
            if (!(t is IDecisionWorkflowTask)) return false;

            return ((IDecisionWorkflowTask) t).CanTransition(instantiator);
        }

        public string TransitionInfo(IWorkflowInstance i)
        {
            if (i.CurrentTask == null) return "";
            if (!(i.CurrentTask is IDecisionWorkflowTask)) return "";

            var instantiator = ((UmbracoWorkflowInstance) i).Instantiator;

            if (!CanTransition(i.CurrentTask, instantiator) && !_isAdmin)
            {
                return "";
            }

            if (i.CurrentTask is UrlGroupDecisionWorkflowTask)
            {
                var urlTask = (UrlGroupDecisionWorkflowTask)i.CurrentTask;
                var url = string.IsNullOrEmpty(urlTask.Url) ? "#" : urlTask.Url;

                return "<a href='" + url + "?id=" + HttpUtility.UrlEncode(i.Id.ToString()) + "' target='_blank'>" + TheGlobalisationService.GetString("transtion") + "</a>";
            }

            if (i.CurrentTask is UrlInstantiatorRestartWorkflowTask)
            {
                var urlTask = (UrlInstantiatorRestartWorkflowTask)i.CurrentTask;
                var url = string.IsNullOrEmpty(urlTask.Url) ? "#" : urlTask.Url;

                return "<a href='" + url + "?id=" + HttpUtility.UrlEncode(i.Id.ToString()) + "' target='_blank'>" + TheGlobalisationService.GetString("restart_workflow") + "</a>";
            }

            var decision = (IDecisionWorkflowTask) i.CurrentTask;
            return "<a href='#' class='wfTransition' rel='"+string.Format(decision.TransitionUrl, HttpUtility.UrlEncode(i.Id.ToString())) +"'>"+ TheGlobalisationService.GetString("transtion") + "</a>";
        }

        protected string AttachmentInfo(IWorkflowInstance i)
        {
            var umbracoWorkflowInstance = (UmbracoWorkflowInstance)i;
            var result = "";

            foreach(var n in umbracoWorkflowInstance.CmsNodes)
            {
                var node = new CMSNode(n);

                if (node.nodeObjectType == Document._objectType)
                {
                    result += "<a href=\"editContent.aspx?id="+ node.Id+"\">" + node.Text + " (" + node.Id + ")</a><br/>";
                }
                else
                {
                    result += node.Text + " (" + node.Id + ")<br/>";
                }
            }
            return result;
        }
    }
}