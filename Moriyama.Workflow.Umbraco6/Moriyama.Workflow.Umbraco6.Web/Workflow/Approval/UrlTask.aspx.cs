using System;
using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Umbraco6.Domain;
using Newtonsoft.Json;
using umbraco.cms.businesslogic;

namespace Moriyama.Workflow.Umbraco6.Web.Workflow.Approval
{
    public partial class UrlTask : System.Web.UI.Page
    {

        public IGlobalisationService TheGlobalisationService { get; set; }

        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            var id = Convert.ToInt32(Request["id"]);
            var workflowIntance = TheWorkflowInstanceService.GetInstance(id);

            var nodes = ((UmbracoWorkflowInstance)workflowIntance).CmsNodes;

            var nodeDetails = new List<NodeInfo>();

            foreach (var nodeId in nodes)
            {
                var node = new CMSNode(nodeId);
                nodeDetails.Add(new NodeInfo {Id = nodeId, Name = node.Text, Approved = true, Comment = string.Empty});
            }

            var json = JsonConvert.SerializeObject(nodeDetails, Formatting.Indented);
            JsonLiteral.Text = json;

            NodeRepeater.DataSource = nodeDetails;
            NodeRepeater.DataBind();

           
        }

        public class NodeInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Approved { get; set; }
            public string Comment { get; set; }
        }
    }
}