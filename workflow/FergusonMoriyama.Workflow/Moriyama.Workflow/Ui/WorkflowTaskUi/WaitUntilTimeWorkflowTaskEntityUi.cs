using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi.Property;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi
{
    public class WaitUntilTimeWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IGlobalisable
    {

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public WaitUntilTimeWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "delayTask");

            TransitionDescriptions.Add("done", TheGlobalisationService.GetString("delay_finished"));

            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(HourPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(MinutePropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(WaitUntilTimeWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("wait_until_time"); }
        }
    }
}
