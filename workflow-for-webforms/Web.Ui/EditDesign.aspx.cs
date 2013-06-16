using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Ui.Factory;
using FergusonMoriyam.Workflow.Ui.Adapter;

namespace Web.Ui
{
    public partial class EditDesign : Page
    {

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
        
        protected override void  OnInit(EventArgs e)
        {
 	        base.OnInit(e);
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
            var tasks = taskTypes.Select(taskType => TheWorkflowTaskFactory.CreateTask(taskType)).ToList();
            
            var adapter = new WorkflowTaskCollectionUiAdapter(tasks);
            TaskInfoLiteral.Text = TheHelper.JsSerializer.Serialize(adapter);

            TaskRepeater.DataSource = adapter.Tasks.Values;
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
            
            foreach(var task in taskInfo.Tasks)
            {
                var props = new Dictionary<string, object>
                                {
                                    {"Name", task.Name}, 
                                    {"Id", new Guid(task.Id)},
                                    {"Description", task.Description},
                                    {"IsStartTask", task.IsStartTask},
                                };

                foreach(var item in task.CustomProperties)
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

            Response.Redirect("Default.aspx");
        }

        protected void CloseWithoutSavingButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}