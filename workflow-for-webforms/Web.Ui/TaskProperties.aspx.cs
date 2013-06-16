using System;
using System.Linq;
using System.Web.UI;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace Web.Ui
{
    public partial class TaskProperties : Page
    {
        private IWorkflowEntityUi _ui;

        public IGlobalisationService TheGlobalisationService { get; set; }
        
        public Helper TheHelper { get; set; }
        public IWorkflowTaskFactory TheTaskFactory { get; set; }
        public IUiResolver TheWorkflowEntityUiResolver { get; set; }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            SaveTaskPropertiesButton.Text = TheGlobalisationService.GetString("save_task_properties");

            var taskType = Request["AssemblyQualifiedTypeName"];
            var task = TheTaskFactory.CreateTask(taskType);

            var queryItems = Request.QueryString.Keys.Cast<string>().ToDictionary<string, string, object>(queryStringKey => queryStringKey, queryStringKey => Request.QueryString[queryStringKey]);
            if(queryItems.ContainsKey("Id"))
            {
                queryItems["Id"] = new Guid((string) queryItems["Id"]);
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