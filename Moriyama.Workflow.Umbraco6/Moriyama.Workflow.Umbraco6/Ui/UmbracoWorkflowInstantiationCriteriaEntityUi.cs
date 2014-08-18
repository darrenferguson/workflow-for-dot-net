using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Ui.Generic;
using Moriyama.Workflow.Ui.WorklowInstantiationCriteriaUi.Property;
using Moriyama.Workflow.Umbraco6.Domain;
using Moriyama.Workflow.Umbraco6.Ui.Property;

namespace Moriyama.Workflow.Umbraco6.Ui
{
    public class UmbracoWorkflowInstantiationCriteriaEntityUi : WorkflowEntityUi
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public UmbracoWorkflowInstantiationCriteriaEntityUi()
        {
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(NamePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(ActivePropertyUi)));

            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(AllowManualInstantiationPropertyUi)));
            
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(WorkflowConfigurationPropertyUi)));
            

            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(EventsPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(CancelEventPropertyUi)));

            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(CriteriaOperandEntityUi)));
            
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(UserTypePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(UserPropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(UmbracoWorkflowInstantiationCriteria);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("umbraco_workflow_instantiation_criteria");  }
        }
    }
}