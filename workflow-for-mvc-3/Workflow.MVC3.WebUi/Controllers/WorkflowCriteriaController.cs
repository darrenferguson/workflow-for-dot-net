using System.Collections.Generic;
using System.Web.Mvc;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using Workflow.MVC3.WebUi.Helpers;

namespace Workflow.MVC3.WebUi.Controllers
{
    public class WorkflowCriteriaController : Controller
    {

        public IWorkflowInstantiationCriteriaService TheWorkflowInstantiationCriteriaService { get; set; }

        public IEventService TheEventService { get; set; }
        public IUiResolver TheWorkflowEntityUiResolver { get; set; }


        public ActionResult Index()
        {
            return View(TheWorkflowInstantiationCriteriaService.List());
        }

        public ActionResult Create()
        {
            
            TheWorkflowInstantiationCriteriaService.CreateWorkflowInstantiationCriteria("New Instantiation Criteria");
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {

            ViewBag.Id = id;

            var instantiationCriteria = TheWorkflowInstantiationCriteriaService.GetCriteria(id);
            var entityUi = TheWorkflowEntityUiResolver.Resolve(instantiationCriteria);

            return View(entityUi.Render(instantiationCriteria));
        }

        public ActionResult Delete(int id)
        {

            TheWorkflowInstantiationCriteriaService.Delete(id); 
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save(int id, FormCollection formCollection)
        {

            var instantiationCriteria = TheWorkflowInstantiationCriteriaService.GetCriteria(id);
            var ui = TheWorkflowEntityUiResolver.Resolve(instantiationCriteria);

            var values = new Dictionary<string, object>();
            foreach (var prop in ui.UiProperties)
            {
                values[prop.PropertyName] = ControlExtensions.ControlValue(prop.RenderControl,
                                                                          formCollection[prop.PropertyName]);
            }

            TheWorkflowInstantiationCriteriaService.SetConfigurationProperties(id, values);
            TheEventService.RegisterEvents();

            return RedirectToAction("Index");
        }

    }
}
