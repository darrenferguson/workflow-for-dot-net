using CustomWorkflowDecisionTask.Task;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi;

namespace CustomWorkflowDecisionTask.Ui
{
    public class CustomGroupDecisionWorkflowTaskEntityUi : GroupDecisionWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(CustomGroupDecisionWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("custom_group_decision"); }
        }
    }
}