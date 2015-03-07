using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientDependency.Core;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Umbraco6.Web.Extensions;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using Umbraco.Core.Persistence;

[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Css.Grid.css", "text/css")]
[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Util.js", "text/javascript")]
[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.Config.js", "text/javascript")]
namespace Moriyama.Workflow.Umbraco6.Web.Workflow
{

    public partial class RecentContent : UserControl
    {
        private class LogEntry
        {
        public int id { get; set; }
        public int userId { get; set; }
        public int NodeId { get; set; }
        public DateTime Datestamp { get; set; }
        public string logHeader { get; set; }
        public string logComment { get; set; }
        }

        public IGlobalisationService TheGlobalisationService { get; set; }

        public const string AdminUserTypeAlias = "admin";

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Css.Grid.css", ClientDependencyType.Css);

            SendToWorkflowButton.Text = TheGlobalisationService.GetString("send_selected_content_to_workflow");

            var user = User.GetCurrent();
            var sevenDaysAgo = DateTime.Now.AddDays(-7);

            var db = new Database("umbracoDbDSN");
            var items = user.UserType.Alias == AdminUserTypeAlias
                ? db.Query<LogEntry>("select * from umbracoLog where logHeader = 'Save' and Datestamp > @0", sevenDaysAgo)
                : db.Query<LogEntry>("select * from umbracoLog where logHeader = 'Save' and Datestamp > @0 and userId = @1", sevenDaysAgo, user.Id);

            //var logItems = user.UserType.Alias == AdminUserTypeAlias 
            //    ? Log.Instance.GetLogItems(LogTypes.Save, sevenDaysAgo)
            //    : Log.Instance.GetLogItems(user, LogTypes.Save, sevenDaysAgo);

            var recentContent = items
                .Select(item => item.NodeId).Distinct()
                .Select(id =>
                {
                    try
                    {
                        return new Document(id);
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }
                })
                .Where(d => d != null)
                .OrderByDescending(d => d.UpdateDate)
                .ToList();

            if (!recentContent.Any()) return;

            MyRecentContentGridView.DataSource = recentContent;
            MyRecentContentGridView.DataBind();
        }

        protected void RecentContentRowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            e.Row.Cells[0].Text = TheGlobalisationService.GetString("id");
            e.Row.Cells[1].Text = TheGlobalisationService.GetString("name");
            e.Row.Cells[2].Text = TheGlobalisationService.GetString("modified");
            e.Row.Cells[3].Text = TheGlobalisationService.GetString("send_to_workflow");
        }

        
        protected void SendToWorkflowButton_Click(object sender, EventArgs e)
        {

        }
    }
}