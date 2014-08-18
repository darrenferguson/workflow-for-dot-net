using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi;
using Moriyama.Workflow.Umbraco6.Task.Ui.Property;

namespace Moriyama.Workflow.Umbraco6.Task.Ui
{
    public class FilterMediaWorkflowTaskEntityUi: BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public FilterMediaWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "filterMediaTask");
            
            UiProperties.Add((IWorkflowUiProperty)CreateGlobalisedObject(typeof(MediaTypePropertyUi)));

            TransitionDescriptions.Add("contains_media", TheGlobalisationService.GetString("contains_media"));
            TransitionDescriptions.Add("does_not_contain_media", TheGlobalisationService.GetString("does_not_contain_media"));
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(FilterMediaWorkflowTask);
        }

        public override string EntityName
        {
            get { return TheGlobalisationService.GetString("filter_media_task"); }
        }  
    
    }
}
