using System.Collections.Generic;
using System.Web.Mvc;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using Workflow.MVC3.WebUi.Helpers;

namespace Workflow.MVC3.WebUi.Controllers
{
    public class WorkflowConfigurationController : Controller
    {
        public IWorkflowConfigurationService TheWorkflowConfigurationService { get; set; }
        public IUiResolver TheWorkflowEntityUiResolver { get; set; }


       

        [HttpGet]
        public ActionResult EditProperties(int id)
        {
            ViewBag.Id = id;

            var config = TheWorkflowConfigurationService.GetConfiguration(id);
            var ui = TheWorkflowEntityUiResolver.Resolve(config);

            return View(ui.Render(config));
        }   
 

        

        [HttpPost]
        public ActionResult SaveProperties(int id, FormCollection formCollection)
        {
           
            var config = TheWorkflowConfigurationService.GetConfiguration(id);
            var ui = TheWorkflowEntityUiResolver.Resolve(config);

            var values = new Dictionary<string, object>();
            foreach(var prop in ui.UiProperties)
            {
                values[prop.PropertyName] = ControlExtensions.ControlValue(prop.RenderControl,
                                                                          formCollection[prop.PropertyName]);
            }
            
            TheWorkflowConfigurationService.SetConfigurationProperties(id, values);
            return RedirectToAction("Index", "Home");
        }
    }
}
