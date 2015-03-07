using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using Moriyama.Workflow.Umbraco6.Ui.Property.Email;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
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