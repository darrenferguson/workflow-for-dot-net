using System.Linq;
using System.Reflection;
using Common.Logging;
using FergusonMoriyam.Workflow.Application;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Application.Interfaces;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using umbraco.BasePages;
using umbraco.BusinessLogic;

namespace FergusonMoriyam.Workflow.Umbraco.Application
{
    public class UmbracoEventService : EventService, IEventService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly UmbracoEventService Service = new UmbracoEventService();
        
        public static UmbracoEventService Instance
        {
            get { return Service; }
        }

        private UmbracoEventService() : base()
        {
        }

        public IUmbracoWorkflowInstantiationCriteriaValidationService TheCriteriaValidationService { get; set; }

        protected override bool ValidateCriteria(IWorkflowInstantiationCriteria criteria)
        {
            var u = User.GetCurrent();
            Log.Debug(string.Format("Validating criteria for user '{0}' - '{1}'", u.LoginName, u.Id));
            return TheCriteriaValidationService.IsCriteriaValid((UmbracoWorkflowInstantiationCriteria) criteria, User.GetCurrent());
        }
        
        protected override void NotifyInstantiation(IWorkflowInstance inst)
        {
            BasePage.Current.ClientTools.ShowSpeechBubble(BasePage.speechBubbleIcon.info,  TheGlobalisationService.GetString("workflow_started"), TheGlobalisationService.GetString("workflow") + string.Format(" '{0}'<br/>(Id -> {1}) {2}.", inst.Name, inst.Id, TheGlobalisationService.GetString("was_started")));
        }

        protected override bool OtherInstancesRunning(IWorkflowInstance workflowInstance, object sender)
        {
            var senderId = (int)TheHelper.GetPropertyValue(sender, "Id");
            var typeName = workflowInstance.GetType().FullName;
            var umbracoWorkflowInstance = (UmbracoWorkflowInstance) workflowInstance;

            // var tenSecondsAgo = DateTime.Now.Subtract(TimeSpan.FromSeconds(10));

            var otherInstances = TheWorkflowInstanceService.ListInstances().Where(
                i => (i.GetType().FullName == typeName)
                    && (((UmbracoWorkflowInstance)i).Instantiator == umbracoWorkflowInstance.Instantiator)
                    && (i.Id != workflowInstance.Id)
                    && (((UmbracoWorkflowInstance)i).CmsNodes.Contains(senderId))
                    // && (i.InstantiationTime > tenSecondsAgo)

            );
            return otherInstances.Count() > 0;
        }

        protected override void AddInstantiatingObjectsToInstance(object sender, IWorkflowInstance inst)
        {
            if(TheHelper.HasProperty(sender, "Id"))
            {
                ((UmbracoWorkflowInstance) inst).CmsNodes.Add(TheHelper.GetProperty<int>(sender, "Id"));
            }
        }
    }
}
