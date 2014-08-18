using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using Moriyama.Workflow.Umbraco6.Ui.Property;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
{
    public class GroupDecisionWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public GroupDecisionWorkflowTaskEntityUi()
            : base()
        {
            
            UiAttributes.Add("class", "groupDecisionTask");

            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(UserTypePropertyUi)));

            TransitionDescriptions.Add("approve", TheGlobalisationService.GetString("approve"));
            TransitionDescriptions.Add("reject", TheGlobalisationService.GetString("reject"));

        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(GroupDecisionWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("group_decision"); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}