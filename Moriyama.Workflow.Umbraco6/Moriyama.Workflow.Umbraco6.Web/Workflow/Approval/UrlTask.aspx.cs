using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Lucene.Net.Documents;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Domain;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using Newtonsoft.Json;
using umbraco.BasePages;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web;
using Content = umbraco.cms.businesslogic.Content;

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
                    var node = new Content(nodeId);

                    if (node.ContentType.Alias.ToLower() != "module")
                    {
                        var url = umbraco.library.NiceUrl(nodeId);

                        nodeDetails.Add(new NodeInfo
                        {
                            Id = nodeId,
                            Name = node.Text,
                            Url = umbraco.library.NiceUrl(nodeId),
                            Approved = true,
                            Comment = string.Empty
                        });
                    }
                    else
                    {
                        var ids = FindReferencesToModule(nodeId, 1050).Distinct();

                        var url = ids.Any() ? umbraco.library.NiceUrl(ids.First()) : "#";

                        nodeDetails.Add(new NodeInfo
                        {
                            Id = ids.Any() ? ids.First() : nodeId,
                            Name = "** " + node.Text,
                            Url = url,
                            Approved = true,
                            Comment = string.Empty,
                            References = string.Join(",", ids)
                        });
                    }
                }

                var json = JsonConvert.SerializeObject(nodeDetails, Formatting.Indented);
                JsonLiteral.Text = json;

                NodeRepeater.DataSource = nodeDetails;
                NodeRepeater.DataBind();
            }
        }

        private List<int> FindReferencesToModule(int moduleId, int parentId)
        {
            var h = new UmbracoHelper(UmbracoContext.Current);

            var root = h.TypedContent(parentId);

            var ids = new List<int>();

                foreach (var child in root.Children)
                {

                    foreach (var property in child.Properties)
                    {
                        var v = property.Value.ToString();
                        if (IsCommaDelimitedIntString(v))
                        {
                            var referenced = v.Split(',').Select(int.Parse);
                            if (referenced.Contains(moduleId))
                            {
                                ids.Add(child.Id);
                            }
                        }
                    }

                    var childIds = FindReferencesToModule(moduleId, child.Id);
                    ids = ids.Concat(childIds).ToList();
                }
            

            return ids;
        }


        private bool IsCommaDelimitedIntString(string value)
        {
            var rgx = new Regex(@"^[\d\,]+$");
            return rgx.IsMatch(value);
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

            var u = umbraco.BusinessLogic.User.GetCurrent();
            transitionComment = "transitioned by <b>" +u.Name + "</b> " + transitionComment;

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

            public string References { get; set; } 
        }
    }
}