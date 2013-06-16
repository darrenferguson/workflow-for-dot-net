using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Ui.Factory;
using FergusonMoriyam.Workflow.Ui.Adapter;
using Workflow.MVC3.WebUi.Models;

namespace Workflow.MVC3.WebUi.Controllers
{
    public class WorkflowDesignController : Controller
    {
        public IUiWorkflowTaskCollectionFactory TheUiWorkflowTaskCollectionFactory { get; set; }
        public ITransitionInfoCollectionFactory TheTransitionInfoCollectionFactory { get; set; }
        public ITaskInfoCollectionFactory TheTaskInfoCollectionFactory { get; set; }

        public IPointService ThePointService { get; set; }
        public IWorkflowConfigurationService TheWorkflowConfigService { get; set; }

        public IPointCollectionFactory ThePointCollectionFactory { get; set; }

        public IWorkflowTaskFactory TheWorkflowTaskFactory { get; set; }

        public IGuidPool TheGuidPool { get; set; }
        public Helper TheHelper { get; set; }

        public ActionResult Index(int id)
        {

            var workflowConfig = TheWorkflowConfigService.GetConfiguration(id);

            var taskTypes = TheHelper.TypesImplementingInterface(typeof(IWorkflowTask));
            var tasks = taskTypes.Select(taskType => TheWorkflowTaskFactory.CreateTask(taskType)).ToList();

            var adapter = new WorkflowTaskCollectionUiAdapter(tasks);


            var ids = workflowConfig.Tasks.Select(task => task.Id.ToString()).ToList();
            var points = ThePointCollectionFactory.Create(ThePointService.GetPoints(ids));

            var instanceTasks = TheUiWorkflowTaskCollectionFactory.Create(workflowConfig.Tasks, points);
           
            var view = new WorkflowDesignViewModel
                           {
                               Id = id,
                               GuidsJson = TheHelper.JsSerializer.Serialize(TheGuidPool.CreateGuids(500)),
                               TaskInfoJson = TheHelper.JsSerializer.Serialize(adapter),
                               AvailableTasks = adapter.Tasks.Values,
                               ConfigurationTasks = instanceTasks.UiTasks.Values,
                               ConfigJson = TheHelper.JsSerializer.Serialize(instanceTasks)

                           };

            return View(view);
        }
        

        public ActionResult Save(int id, FormCollection formCollection)
        {

          var taskInfo = TheTaskInfoCollectionFactory.Parse(formCollection["taskInfo"]);
          var transitionInfo = TheTransitionInfoCollectionFactory.Parse(formCollection["transitionInfo"]);

          var ids = taskInfo.Tasks.Select(task => task.Id).ToList();


          ThePointService.DeletePoints(ids);
          TheWorkflowConfigService.RemoveTasks(id);
          TheWorkflowConfigService.RemoveTransitions(id);


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
              TheWorkflowConfigService.AddTask(id, task.AssemblyQualifiedTypeName, props);
          }

          foreach (var trans in transitionInfo.Transitions)
          {
              TheWorkflowConfigService.AddTransition(id, trans.Source, trans.Target, trans.Transition);
          }
          
          return View("Close"); 
        }

        public ActionResult Close(int id)
        {
            return View();
        }

    }
}
