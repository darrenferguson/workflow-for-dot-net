using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property.Email;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi
{
    public abstract class BaseEmailWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService { get; set; }

        protected BaseEmailWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "basicEmailTask");

            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(SubjectPropertyUi)));

            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(MailInstantiatorPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(MailOwnersPropertyUi)));



            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(UserPropertyUi)));
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(UserTypePropertyUi)));
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(FromPropertyUi)));

            TransitionDescriptions.Add("done", TheGlobalisationService.GetString("email_sent"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(BaseEmailWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("email_task"); }
        }
    }
}