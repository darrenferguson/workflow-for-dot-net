using System;
using System.Web.Mvc;
using FergusonMoriyam.Workflow.Interfaces.Application;
using Workflow.MVC3.WebUi.Models;
using Workflow.MVC3.WebUi.Models.Post;

namespace Workflow.MVC3.WebUi.Controllers
{
    public class HomeController : Controller
    {
        public IWorkflowConfigurationService TheWorkflowConfigurationService { get; set; }
        
        public ActionResult Index()
        {
            return View(new WorkflowConfigurationViewModel { Configurations = TheWorkflowConfigurationService.ListConfigurations() });
        }

        [HttpPost]
        public ActionResult Create(WorkflowConfigurationCreateModel model)
        {
            
            var workflowName = string.Format("New Workflow - {0}", DateTime.Now);

            TheWorkflowConfigurationService.CreateWorkflowConfiguration(workflowName, model.Type);
        
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Instantiate(int id)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            TheWorkflowConfigurationService.DeleteWorkflowConfiguration(id);
            return RedirectToAction("Index");
        }
       
    }
}
