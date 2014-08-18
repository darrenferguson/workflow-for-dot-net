using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using Moriyama.Workflow.Umbraco6.Ui.Property;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
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