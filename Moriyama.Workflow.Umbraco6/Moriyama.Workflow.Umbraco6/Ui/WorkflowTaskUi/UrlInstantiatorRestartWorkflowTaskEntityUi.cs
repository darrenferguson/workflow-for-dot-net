using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Task;
using Moriyama.Workflow.Umbraco6.Ui.Property;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
{
    public class UrlInstantiatorRestartWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public UrlInstantiatorRestartWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "userDecisionTask");
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(UrlPropertyUi)));

            TransitionDescriptions.Add("restart_workflow", TheGlobalisationService.GetString("restart_workflow"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(UrlInstantiatorRestartWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("instantiator_restart"); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}
