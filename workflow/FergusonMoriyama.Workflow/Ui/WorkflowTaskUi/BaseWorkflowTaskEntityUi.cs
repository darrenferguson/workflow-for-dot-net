using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.Generic;
using FergusonMoriyam.Workflow.Ui.WorkflowtaskUi.Property;

namespace FergusonMoriyam.Workflow.Ui.WorkflowTaskUi
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
