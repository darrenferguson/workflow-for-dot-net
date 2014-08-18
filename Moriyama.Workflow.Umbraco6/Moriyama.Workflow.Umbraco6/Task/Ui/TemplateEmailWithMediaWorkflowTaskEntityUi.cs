using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Umbraco6.Ui.Property;
using Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi;

namespace Moriyama.Workflow.Umbraco6.Task.Ui
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
