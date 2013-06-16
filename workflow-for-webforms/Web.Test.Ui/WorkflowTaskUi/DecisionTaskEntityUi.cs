using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi;
using FergusonMoriyam.Workflow.Web.Domain.Task;

namespace FergusonMoriyam.Workflow.Web.Test.Ui.WorkflowTaskUi
{
    public class DecisionTaskEntityUi : BaseWorkflowTaskEntityUi, IWorkflowTaskEntityUi
    {
        public DecisionTaskEntityUi()
            : base()
        {
            UiAttributes.Add("class", "decisionTask");

            TransitionDescriptions.Add("yes", "Yes");
            TransitionDescriptions.Add("no", "No");
        }

        public override bool SupportsType(object o)
        {
            return o.GetType() == typeof(DecisionTask);
        }

        public override string EntityName
        {
            get { return "Decision Task"; }
        }
    }
}
