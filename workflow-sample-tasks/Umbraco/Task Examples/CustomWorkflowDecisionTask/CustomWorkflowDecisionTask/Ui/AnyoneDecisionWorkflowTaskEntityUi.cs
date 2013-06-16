using CustomWorkflowDecisionTask.Task;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;

namespace CustomWorkflowDecisionTask.Ui
{
    public class AnyoneDecisionWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public AnyoneDecisionWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "userDecisionTask");
            
            TransitionDescriptions.Add("approve", TheGlobalisationService.GetString("approve"));
            TransitionDescriptions.Add("reject", TheGlobalisationService.GetString("reject"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(AnyoneDecisionWorkflowTask);
        }
        
        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("anyone_decides"); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}