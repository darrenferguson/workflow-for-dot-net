using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Ui.Generic;
using Moriyama.Workflow.Ui.WorkflowConfigurationUi;
using Moriyama.Workflow.Umbraco6.Domain;

namespace Moriyama.Workflow.Umbraco6.Ui
{
    public class WorkflowConfigurationEntityUi : WorkflowEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get; set;
        }

        public WorkflowConfigurationEntityUi()
        {
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(NamePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(IsConfigurationActivePropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(UmbracoWorkflowConfiguration);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("workflow_config"); }
        }

       
    }
}
