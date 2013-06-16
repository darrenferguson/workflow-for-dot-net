using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using FergusonMoriyam.Workflow.Web.Domain.Task;

namespace FergusonMoriyam.Workflow.Web.Test.Ui.WorkflowTaskUi
{
    public class ExampleRunnableTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi
    {
        public ExampleRunnableTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "runnableTask");

            TransitionDescriptions.Add("done", "Done");
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(ExampleRunnableTask);
        }

        public override string EntityName
        {
            get { return "Example Runnable Task"; }
        }
    }
}
