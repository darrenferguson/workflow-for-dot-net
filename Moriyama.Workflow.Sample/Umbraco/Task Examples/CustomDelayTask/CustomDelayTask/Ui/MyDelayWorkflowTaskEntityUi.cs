using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;

namespace CustomDelayTask.Ui
{
    public class MyDelayWorkflowTaskEntityUi : BaseWorkflowTaskEntityUi
    {
        public MyDelayWorkflowTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "delayTask");
            TransitionDescriptions.Add("done", "Delay Finished");
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof (MyDelayWorkflowTask);
        }

        public override string EntityName
        {
            get { return "Wait a Minute"; }
        }
    }
}
