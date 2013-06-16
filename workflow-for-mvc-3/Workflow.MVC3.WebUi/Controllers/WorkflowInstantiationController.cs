using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;

namespace Workflow.MVC3.WebUi.Controllers
{
    public class WorkflowInstantiationController : Controller
    {
        public IWorkflowInstanceService TheWorkflowInstanceService { get; set; }
        public IWorkflowRuntime TheWorkflowRuntime { get; set; }

        public ActionResult Index()
        {
            return View(TheWorkflowInstanceService.ListInstances());
        }

        [HttpGet]
        public ActionResult Instantiate(int id)
        {

            TheWorkflowInstanceService.Instantiate(id);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            TheWorkflowInstanceService.DeleteWorkflowInstance(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Start(int id)
        {
            TheWorkflowInstanceService.Start(id);
            TheWorkflowRuntime.RunWorkflows();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Run()
        {
            TheWorkflowRuntime.RunWorkflows();
            return RedirectToAction("Index");
        }
    }
}
