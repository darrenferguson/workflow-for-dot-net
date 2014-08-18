using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Umbraco6.Ui.Property;
using Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi;

namespace Moriyama.Workflow.Umbraco6.Task.Ui
{
    public class TemplateEmailWorkflowTaskEntityUi : BaseEmailWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public TemplateEmailWorkflowTaskEntityUi()
            : base()
        {
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(TemplatePropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(TemplateEmailWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("template_email_task"); }
        }



    }
}
