using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi
{
    public class UserDecisionWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public UserDecisionWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "userDecisionTask");
            
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(UserPropertyUi)));

            TransitionDescriptions.Add("approve", TheGlobalisationService.GetString("approve"));
            TransitionDescriptions.Add("reject", TheGlobalisationService.GetString("reject"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(UserDecisionWorkflowTask);
        }

        
        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("user_decision"); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}