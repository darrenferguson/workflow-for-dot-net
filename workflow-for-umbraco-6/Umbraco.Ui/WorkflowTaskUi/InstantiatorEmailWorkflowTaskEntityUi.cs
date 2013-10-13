using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property.Email;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi
{
    public class InstantiatorEmailWorkflowTaskEntityUi : BaseEmailWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {

        public InstantiatorEmailWorkflowTaskEntityUi() : base()
            
        {
            UiAttributes.Add("class", "basicEmailTask");

            UiProperties = new List<IWorkflowUiProperty>
                               {
                                   (IWorkflowUiProperty) CreateGlobalisedObject(typeof (SubjectPropertyUi)),
                                   (IWorkflowUiProperty) CreateGlobalisedObject(typeof (FromPropertyUi)),
                                   (IWorkflowUiProperty) CreateGlobalisedObject(typeof (BodyPropertyUi))
                               };

            TransitionDescriptions.Add("done", TheGlobalisationService.GetString("email_sent"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(InstantiatorEmailWorkflowTask);
        }
    }
}