using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace TwitterTask.UiProperty
{
    public class ConsumerKeyPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        public override string PropertyName
        {
            get { return "ConsumerKey"; }
        }

        public string Label
        {
            get { return "Consumer Key"; }
        }
    }
}
