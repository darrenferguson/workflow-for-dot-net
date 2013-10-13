using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientDependency.Core;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Umbraco.Web.Ui.Extensions;
using umbraco.BasePages;

[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery-1.5.1.min.js", "text/javascript")]
namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
{
    public partial class EditTaskProperties : UmbracoEnsuredPage
    {
        private IWorkflowEntityUi _ui;

        public IGlobalisationService TheGlobalisationService { get; set; }

        public Helper TheHelper { get; set; }
        public IWorkflowTaskFactory TheTaskFactory { get; set; }
        public IUiResolver TheWorkflowEntityUiResolver { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery-1.5.1.min.js", ClientDependencyType.Javascript);
            SaveTaskPropertiesButton.Text = TheGlobalisationService.GetString("save_task_properties");

        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            var taskType = Request["AssemblyQualifiedTypeName"];
            var task = TheTaskFactory.CreateTask(taskType);

            var queryItems = new Dictionary<string, object>();
            foreach(string key in Request.QueryString.Keys)
            {
                if (key.Contains("[]"))
                {
                    var dictKey = key.Replace("[]", "");
                    var items = Request.QueryString[key].Split(',');

                    try
                    {
                        queryItems.Add(dictKey, items.ToList().ConvertAll(Convert.ToInt32));
                    } catch
                    {
                        queryItems.Add(dictKey, items.ToList().ConvertAll(Convert.ToString));
                    }

                } else
                {
                    queryItems.Add(key, Request.QueryString[key]);
                }
            }
            
            if (queryItems.ContainsKey("Id"))
            {
                queryItems["Id"] = new Guid((string)queryItems["Id"]);
            }
                  
            TheHelper.SetProperties(task, queryItems);
            _ui = TheWorkflowEntityUiResolver.Resolve(task);

            foreach (var c in _ui.Render(task))
            {
                TaskPropertiesUiPanel.Controls.Add(c);
            }
        }

        protected void SaveTaskPropertiesButtonClick(object sender, EventArgs e)
        {
            var values = _ui.UiProperties.ToDictionary(uiProperty => uiProperty.PropertyName, uiProperty => uiProperty.Value);
            TaskPropertiesLiteral.Text = @"wf.taskProps = " + TheHelper.JsSerializer.Serialize(values);
        }
    }
}