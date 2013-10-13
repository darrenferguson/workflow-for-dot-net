using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;
using FergusonMoriyam.Workflow.Umbraco.Ui.Property;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi
{
    public class PublishDocumentsWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public PublishDocumentsWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "publishDocumentsTask");
            
            
            TransitionDescriptions.Add("done", TheGlobalisationService.GetString("published"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(PublishDocumentsWorkflowTask);
        }

        
        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("publish_documents"); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}