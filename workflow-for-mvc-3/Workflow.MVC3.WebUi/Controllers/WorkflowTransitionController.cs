using System.Web.Mvc;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using Workflow.MVC3.WebUi.Models;

namespace Workflow.MVC3.WebUi.Controllers
{
    public class WorkflowTransitionController : Controller
    {
        public IWorkflowTaskTransitionService TheTransitionService { get; set; }
       
        [HttpGet]
        public ActionResult Index(int id)
        {
            var workflowInstance = TheTransitionService.GetWorkflowInstance(id);

            var model = new WorkflowTransitionViewModel
                            {
                                InstanceId = id,
                                CanTransition = TheTransitionService.CanTransition(workflowInstance),
                                Transitions = null
                            };

            if(model.CanTransition)
            {
                model.Transitions = TheTransitionService.GetTransitions(workflowInstance);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Transition(int id, PostTransitionModel model)
        {
            var workflowInstance = TheTransitionService.GetWorkflowInstance(id);
            TheTransitionService.Transition(workflowInstance, model.Transition);

            return RedirectToAction("Index", "WorkflowInstantiation");
        }

    }
}
