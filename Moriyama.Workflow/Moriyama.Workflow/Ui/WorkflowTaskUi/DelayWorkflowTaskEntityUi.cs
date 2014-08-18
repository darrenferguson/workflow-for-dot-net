using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi.Property;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi
{
    public class DelayWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public DelayWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "delayTask");

            TransitionDescriptions.Add("done", TheGlobalisationService.GetString("delay_finished"));

            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(DelayTaskUnitPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(DelayTaskIntervalPropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(DelayWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("delay_task"); }
        }
    }
}
