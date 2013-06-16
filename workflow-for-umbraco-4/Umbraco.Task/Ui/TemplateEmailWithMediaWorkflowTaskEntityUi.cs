using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property;
using FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi;

namespace FergusonMoriyam.Workflow.Umbraco.Task.Ui
{
    public class TemplateEmailWithMediaWorkflowTaskEntityUi : BaseEmailWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public TemplateEmailWithMediaWorkflowTaskEntityUi()
            : base()
        {
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(TemplatePropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(TemplateEmailWithMediaWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("template_email_task_media_attached"); }
        }
    }
}
