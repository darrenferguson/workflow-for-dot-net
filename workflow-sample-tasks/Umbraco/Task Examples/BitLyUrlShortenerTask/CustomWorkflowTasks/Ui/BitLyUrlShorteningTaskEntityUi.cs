using CustomWorkflowTasks.UiProperty;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;

namespace CustomWorkflowTasks.Ui
{
    // An Entity Ui describes how the workflow task is rendered in the workflow Designer
    public class BitLyUrlShorteningTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {
        public BitLyUrlShorteningTaskEntityUi()
            : base()
        {
            // Describe the workflow transition
            TransitionDescriptions.Add("done", "URL Was shortened");

            // Decorate your task with a custom CSS class in the designer.
            // The workflow designer scans ~/umbraco/plugins/fmworkflow/css for custom CSS files and includes all of them.
            UiAttributes.Add("class", "bitlyTask");
            
            // These properties explain how to present a Ui to get values for the public properties of the workflow task.
            UiProperties.Add(new BitLyApiKeyPropertyUi());
            UiProperties.Add(new BitLyLoginPropertyUi());
            UiProperties.Add(new DocumentTypePropertyUi());
        }
        
        // When passed an object returns a bool indicating whether this task can supply a UI for it.
        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(BitLyUrlShorteningTask);
        }

        // Name of task
        public override string EntityName
        {
            get { return "Bit.ly URL Shorten"; }
        }

        // If a class implements IGlobalisable the workflow runtime will pass an IGlobalisationService so you can
        // use the appropriate language when returning strings. it isn't used in this sample.
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}
