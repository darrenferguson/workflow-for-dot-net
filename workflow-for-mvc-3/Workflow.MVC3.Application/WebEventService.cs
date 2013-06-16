using FergusonMoriyam.Workflow.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace Workflow.MVC3.Application
{
    public class WebEventService : EventService
    {
        private static readonly WebEventService Service = new WebEventService();

        public static WebEventService Instance
        {
            get { return Service; }
        }

        protected WebEventService()
        {
        }

        protected override void AddInstantiatingObjectsToInstance(object sender, IWorkflowInstance inst)
        {
        }

        protected override bool ValidateCriteria(IWorkflowInstantiationCriteria criteria)
        {
            return true;
        }

        protected override bool OtherInstancesRunning(IWorkflowInstance inst, object sender)
        {
            return false;
        }
    }
}
