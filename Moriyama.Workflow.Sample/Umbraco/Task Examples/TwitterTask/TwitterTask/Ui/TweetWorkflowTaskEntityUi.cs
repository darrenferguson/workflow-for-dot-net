using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using TwitterTask.UiProperty;

namespace TwitterTask.Ui
{
    // An Entity Ui describes how the workflow task is rendered in the workflow Designer
    public class TweetWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi, IGlobalisable
    {

        public TweetWorkflowTaskEntityUi()
            : base()
        {
            // Describe the workflow transition
            TransitionDescriptions.Add("done", "Task was tweeted");

            // Decorate your task with a custom CSS class in the designer.
            // The workflow designer scans ~/umbraco/plugins/fmworkflow/css for custom CSS files and includes all of them.
            UiAttributes.Add("class", "tweetTask");
            
            // These properties explain how to present a Ui to get values for the public properties of the workflow task.
            UiProperties.Add(new AccessTokenPropertyUi());
            UiProperties.Add(new AccessTokenSecretPropertyUi());

            UiProperties.Add(new ConsumerKeyPropertyUi());
            UiProperties.Add(new ConsumerSecretPropertyUi());

            UiProperties.Add(new ShortUrlPropertyUi());
            UiProperties.Add(new TweetTextPropertyUi());
        }

        // When passed an object returns a bool indicating whether this task can supply a UI for it.
        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(TweetWorkflowTask);
        }

        // Name of task
        public override string EntityName
        {
            get { return "Tweet Task"; }
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
