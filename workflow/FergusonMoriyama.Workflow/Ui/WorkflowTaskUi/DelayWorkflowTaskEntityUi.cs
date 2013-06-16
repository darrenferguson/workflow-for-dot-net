using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi.Property;

namespace FergusonMoriyam.Workflow.Ui.WorkflowTaskUi
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
