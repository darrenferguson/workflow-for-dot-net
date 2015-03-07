using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using Moriyama.Workflow.Umbraco6.Ui.Property;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
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