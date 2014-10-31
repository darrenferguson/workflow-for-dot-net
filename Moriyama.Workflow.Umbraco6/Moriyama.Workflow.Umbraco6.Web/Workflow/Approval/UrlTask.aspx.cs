using System;
using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Domain;
using Newtonsoft.Json;
using umbraco.cms.businesslogic;
using Umbraco.Core.Logging;
using Umbraco.Web.UI.Pages;

namespace Moriyama.Workflow.Umbraco6.Web.Workflow.Approval
{
    public partial class UrlTask : UmbracoEnsuredPage
    {

        public IGlobalisationService TheGlobalisationService { get; set; }

        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }

        public IWorkflowRuntime TheWorkflowRuntime { get; set; }

        private IWorkflowInstance _workflowInstance;

        protected void Page_Load(object sender, EventArgs e)
        {

            var id = Convert.ToInt32(Request["id"]);
            _workflowInstance = TheWorkflowInstanceService.GetInstance(id);

            var nodes = ((UmbracoWorkflowInstance)_workflowInstance).CmsNodes;

            if (!Page.IsPostBack)
            {
                var nodeDetails = new List<NodeInfo>();

                foreach (var nodeId in nodes)
                {
                    var node = new CMSNode(nodeId);
                    nodeDetails.Add(new NodeInfo
                    {
                        Id = nodeId,
                        Name = node.Text,
                        Url = umbraco.library.NiceUrl(nodeId),
                        Approved = true,
                        Comment = string.Empty
                    });
                }

                var json = JsonConvert.SerializeObject(nodeDetails, Formatting.Indented);
                JsonLiteral.Text = json;

                NodeRepeater.DataSource = nodeDetails;
                NodeRepeater.DataBind();
            }
        }

        public void BtnClick(Object sender,EventArgs e)
        {
            var json = JsonField.Value;
            var deserialised = JsonConvert.DeserializeObject<IEnumerable<NodeInfo>>(json);

            var task = _workflowInstance.CurrentTask.Name;

            var user = umbraco.BusinessLogic.User.GetCurrent();

            var proceed = true;
            var transitionComment = Environment.NewLine + "<br/>";

            foreach (var item in deserialised)
            {
                LogHelper.Info(GetType(), "\"" + task + "\",\"" + item.Name + "\"," + item.Id + "," + (item.Approved ? "Approved" : "Rejected") + ",\"" + item.Comment + "\"");

                if (!item.Approved)
                {
                    proceed = false;
                    transitionComment += Environment.NewLine + "<br/>" + "Page: " + item.Name + "(" + item.Id + ") was rejected by "+ user.Name + "\"" + item.Comment + "\"" + Environment.NewLine + "<br/>";
                }

                //if (item.Approved) continue;

                //var umbracoWorkflowInstance = (UmbracoWorkflowInstance) _workflowInstance;
                //umbracoWorkflowInstance.CmsNodes.Remove(item.Id);

                //if (umbracoWorkflowInstance.CmsNodes.Count < 1)
                //{
                //    forceEndWorkflow = true;
                //}
            }

            var transition = proceed ? "approve" : "reject";
            TheWorkflowRuntime.Transition(_workflowInstance, _workflowInstance.CurrentTask, transition, transitionComment);

            ClosePanel.Visible = true;
        }

        public class NodeInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
            public bool Approved { get; set; }
            public string Comment { get; set; }
        }
    }
}