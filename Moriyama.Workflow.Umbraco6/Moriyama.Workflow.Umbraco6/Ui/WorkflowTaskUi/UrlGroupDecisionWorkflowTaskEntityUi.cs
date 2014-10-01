using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Task;
using Moriyama.Workflow.Umbraco6.Ui.Property;
using Moriyama.Workflow.Umbraco6.Ui.Property.Email;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
{
    public class UrlGroupDecisionWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {

        public UrlGroupDecisionWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "groupDecisionTask");

            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(UserTypePropertyUi)));

            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(UrlPropertyUi)));

            TransitionDescriptions.Add("approve", TheGlobalisationService.GetString("approve"));
            TransitionDescriptions.Add("reject", TheGlobalisationService.GetString("reject"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(UrlGroupDecisionWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("url_group_decision"); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}
