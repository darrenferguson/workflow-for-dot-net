using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using FergusonMoriyam.Workflow.Application.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Domain.Factory;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using Workflow.MVC3.WebUi.Helpers;

namespace Workflow.MVC3.WebUi.Controllers
{
    public class WorkflowTaskPropertiesController : Controller
    {
        public Helper TheHelper { get; set; }
        public IWorkflowTaskFactory TheTaskFactory { get; set; }
        public IUiResolver TheWorkflowEntityUiResolver { get; set; }

        public ActionResult Index(string id)
        {
            var formCollection = new FormCollection(Request.QueryString);
            ViewBag.Id = id;

            var taskType = formCollection["AssemblyQualifiedTypeName"];
            ViewBag.TaskType = taskType;
            
            var task = TheTaskFactory.CreateTask(taskType);

            var ui = TheWorkflowEntityUiResolver.Resolve(task);

            var values = new Dictionary<string, object>();
            foreach (var prop in ui.UiProperties)
            {
                values[prop.PropertyName] = ControlExtensions.ControlValue(prop.RenderControl,
                                                                          formCollection[prop.PropertyName]);
            }
            if (values.ContainsKey("Id"))
            {
                values["Id"] = new Guid((string)values["Id"]);
            }

            TheHelper.SetProperties(task, values);
            ViewBag.PropertiesJson = TheHelper.JsSerializer.Serialize(values);

            return View(ui.Render(task));
        }
    }
}
