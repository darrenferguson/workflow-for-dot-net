using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.Generic;
using Moriyama.Workflow.Ui.WorkflowtaskUi.Property;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi
{
    public abstract class BaseWorkflowTaskEntityUi : WorkflowEntityUi, IWorkflowTaskEntityUi
    {
        
        protected BaseWorkflowTaskEntityUi()
        {
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(IsStartTaskPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(TaskNamePropertyUi)));
        }
    }
}
