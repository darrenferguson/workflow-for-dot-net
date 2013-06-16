using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;
using FergusonMoriyam.Workflow.Ui.Generic;
using FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Property;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property;

namespace FergusonMoriyam.Workflow.Umbraco.Ui
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