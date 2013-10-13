using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using ClientDependency.Core;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Ui.Factory;
using FergusonMoriyam.Workflow.Ui.Adapter;
using FergusonMoriyam.Workflow.Umbraco.Web.Ui.Extensions;
using Common.Logging;
using umbraco.BasePages;
using umbraco.IO;

[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Css.Designer.css", "text/css")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery-1.5.1.min.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery-ui-1.8.12.custom.min.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery.jsPlumb-1.3.2-all-min.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.json2.js", "text/javascript")]
[assembly: WebResource("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.workflow.js", "text/javascript")]


namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
{
    public partial class EditDesign : UmbracoEnsuredPage
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public IGlobalisationService TheGlobalisationService { get; set; }


        public IUiWorkflowTaskCollectionFactory TheUiWorkflowTaskCollectionFactory { get; set; }
        public ITransitionInfoCollectionFactory TheTransitionInfoCollectionFactory { get; set; }
        public ITaskInfoCollectionFactory TheTaskInfoCollectionFactory { get; set; }

        public IPointService ThePointService { get; set; }
        public IWorkflowConfigurationService TheWorkflowConfigService { get; set; }

        public IPointCollectionFactory ThePointCollectionFactory { get; set; }

        public IWorkflowTaskFactory TheWorkflowTaskFactory { get; set; }

        public IGuidPool TheGuidPool { get; set; }
        public Helper TheHelper { get; set; }

        private int _workflowId;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Css.Designer.css", ClientDependencyType.Css);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery-1.5.1.min.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery-ui-1.8.12.custom.min.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.jquery.jsPlumb-1.3.2-all-min.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.json2.js", ClientDependencyType.Javascript);
            this.AddResourceToClientDependency("FergusonMoriyam.Workflow.Umbraco.Web.Ui.Js.workflow.js", ClientDependencyType.Javascript);

            var userCssPath = IOHelper.MapPath("~/umbraco/plugins/fmworkflow/css");
            

            foreach(var file in Directory.GetFiles(userCssPath))
            {
                var name = new FileInfo(file).Name;
                Header.Controls.Add(
                            new LiteralControl("<link type='text/css' rel='stylesheet' href='css/" + name + "' />"));
                        
            }
               
            _workflowId = Convert.ToInt32(Request["id"]);

            SaveButton.Text = TheGlobalisationService.GetString("save_design_and_close");
            CloseWithoutSavingButton.Text = TheGlobalisationService.GetString("close_without_saving");
            TaskMessageLiteral.Text = TheGlobalisationService.GetString("drag_and_drop_tasks");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GuidPoolLiteral.Text = TheHelper.JsSerializer.Serialize(TheGuidPool.CreateGuids(500));

            var taskTypes = TheHelper.TypesImplementingInterface(typeof(IWorkflowTask));
            Log.Debug(string.Format("Found {0} types implementing IWorkflowTask", taskTypes.Count()));

            var tasks = taskTypes.Select(taskType => TheWorkflowTaskFactory.CreateTask(taskType)).ToList();

            var adapter = new WorkflowTaskCollectionUiAdapter(tasks);

            TaskInfoLiteral.Text = TheHelper.JsSerializer.Serialize(adapter);
            var data = adapter.Tasks.Values.OrderBy(v => v.Name);
            
            TaskRepeater.DataSource = data;
            TaskRepeater.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var workflowConfig = TheWorkflowConfigService.GetConfiguration(_workflowId);

            var ids = workflowConfig.Tasks.Select(task => task.Id.ToString()).ToList();
            var points = ThePointCollectionFactory.Create(ThePointService.GetPoints(ids));

            var tasks = TheUiWorkflowTaskCollectionFactory.Create(workflowConfig.Tasks, points);
            WorkflowConfigLiteral.Text = TheHelper.JsSerializer.Serialize(tasks);

            TaskInstanceRepeater.DataSource = tasks.UiTasks.Values;
            TaskInstanceRepeater.DataBind();

            TaskMessageLiteral.Visible = (tasks.UiTasks.Values.Count == 0);
        }

        protected void SaveButtonClick(object sender, EventArgs e)
        {
            var taskInfo = TheTaskInfoCollectionFactory.Parse(Tasks.Value);
            var transitionInfo = TheTransitionInfoCollectionFactory.Parse(Transitions.Value);

            var ids = taskInfo.Tasks.Select(task => task.Id).ToList();

            ThePointService.DeletePoints(ids);
            TheWorkflowConfigService.RemoveTasks(_workflowId);
            TheWorkflowConfigService.RemoveTransitions(_workflowId);

            foreach (var task in taskInfo.Tasks)
            {
                var props = new Dictionary<string, object>
                                {
                                    {"Name", task.Name}, 
                                    {"Id", new Guid(task.Id)},
                                    {"Description", task.Description},
                                    {"IsStartTask", task.IsStartTask},
                                };

                foreach (var item in task.CustomProperties)
                {
                    props.Add(item.Key, item.Value);
                }

                ThePointService.AddPoint(task.Id, task.Top, task.Left);
                TheWorkflowConfigService.AddTask(_workflowId, task.AssemblyQualifiedTypeName, props);
            }

            foreach (var trans in transitionInfo.Transitions)
            {
                TheWorkflowConfigService.AddTransition(_workflowId, trans.Source, trans.Target, trans.Transition);
            }

            CloseWindowLiteral.Visible = true;
        }

        protected void CloseWithoutSavingButtonClick(object sender, EventArgs e)
        {
            CloseWindowLiteral.Visible = true;
        }
    }
}