using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi
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