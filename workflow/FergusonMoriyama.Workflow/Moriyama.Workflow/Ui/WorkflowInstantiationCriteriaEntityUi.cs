using Moriyama.Workflow.Domain;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.Generic;
using Moriyama.Workflow.Ui.WorklowInstantiationCriteriaUi.Property;

namespace Moriyama.Workflow.Ui
{
    public class WorkflowInstantiationCriteriaEntityUi : WorkflowEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public WorkflowInstantiationCriteriaEntityUi()
        {
           
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(NamePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(EventsPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(CancelEventPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(ActivePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty) CreateGlobalisedObject(typeof(WorkflowConfigurationPropertyUi)));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(WorkflowInstantiationCriteria);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("instantiation_criteria"); }
        } 
    }
}
