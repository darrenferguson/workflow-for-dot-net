using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.Generic;
using FergusonMoriyam.Workflow.Ui.WorkflowConfigurationUi;

namespace FergusonMoriyam.Workflow.Ui
{
    public class WorkflowConfigurationEntityUi : WorkflowEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public WorkflowConfigurationEntityUi()
        {
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(NamePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(IsConfigurationActivePropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(WorkflowConfiguration);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("workflow_configuration"); }
        }
    }
}
