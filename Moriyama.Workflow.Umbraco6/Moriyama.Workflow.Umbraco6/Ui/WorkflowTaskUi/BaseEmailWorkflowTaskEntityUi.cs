using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using Moriyama.Workflow.Umbraco6.Ui.Property;
using Moriyama.Workflow.Umbraco6.Ui.Property.Email;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
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