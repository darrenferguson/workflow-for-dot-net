using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi.Property;
using Moriyama.Workflow.Ui.WorkflowtaskUi.Property;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi
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
