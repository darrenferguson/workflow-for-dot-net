using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property.Email;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi
{
    public class BasicEmailWorkflowTaskEntityUi : BaseEmailWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
       
        public BasicEmailWorkflowTaskEntityUi()
            : base()
        {
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(BodyPropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(BasicEmailWorkflowTask);
        }
    }
}