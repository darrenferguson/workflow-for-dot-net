using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi.Property;
using FergusonMoriyam.Workflow.Ui.WorkflowtaskUi.Property;

namespace FergusonMoriyam.Workflow.Ui.WorkflowTaskUi
{
    public class EndWorkflowTaskEntityUi : WorkflowEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public EndWorkflowTaskEntityUi() : base()
        {
            UiAttributes.Add("class", "endTask");
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(TaskNamePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(TaskDescriptionPropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(EndWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("end_task"); }
        }
    }
}
