using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property;
using FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi;

namespace FergusonMoriyam.Workflow.Umbraco.Task.Ui
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
