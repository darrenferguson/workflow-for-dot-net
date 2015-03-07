using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace TwitterTask.UiProperty
{
    public class ConsumerSecretPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        public override string PropertyName
        {
            get { return "ConsumerSecret"; }
        }

        public string Label
        {
            get { return "Consumer Secret"; }
        }
    }
}
