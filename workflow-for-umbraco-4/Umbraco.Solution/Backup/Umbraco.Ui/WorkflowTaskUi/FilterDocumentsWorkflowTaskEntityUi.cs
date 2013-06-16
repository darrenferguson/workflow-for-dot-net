using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;
using FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi.Property;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.WorkflowTaskUi
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