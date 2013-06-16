using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi.Property;

namespace FergusonMoriyam.Workflow.Ui.WorkflowTaskUi
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
