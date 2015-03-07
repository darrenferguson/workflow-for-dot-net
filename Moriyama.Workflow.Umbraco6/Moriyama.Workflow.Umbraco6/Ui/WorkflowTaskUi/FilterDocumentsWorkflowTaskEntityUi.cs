using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Domain.Task;
using Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi.Property;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi
{
    public class FilterDocumentsWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public FilterDocumentsWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "filterDocumentsTask");
            
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(DocumentTypePropertyUi)));

            TransitionDescriptions.Add("contains_docs", TheGlobalisationService.GetString("contains_docs"));
            TransitionDescriptions.Add("does_not_contain_docs", TheGlobalisationService.GetString("does_not_contain_docs"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(FilterDocumentsWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("filter_docs_task"); }
        }  
    }
}