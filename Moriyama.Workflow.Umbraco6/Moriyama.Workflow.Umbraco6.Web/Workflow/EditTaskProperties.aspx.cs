using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using ClientDependency.Core;
using Moriyama.Workflow.Application.Reflection;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Domain.Factory;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Umbraco6.Web.Extensions;
using Umbraco.Web.UI.Pages;


[assembly: WebResource("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.jquery-1.5.1.min.js", "text/javascript")]
namespace Moriyama.Workflow.Umbraco6.Web.Workflow
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

            this.AddResourceToClientDependency("Moriyama.Workflow.Umbraco6.Web.Workflow.Js.jquery-1.5.1.min.js", ClientDependencyType.Javascript);
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