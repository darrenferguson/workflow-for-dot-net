using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace TwitterTask.UiProperty
{
    public class AccessTokenSecretPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        public override string PropertyName
        {
            get { return "AccessTokenSecret"; }
        }

        public string Label
        {
            get { return "Access Token Secret"; }
        }
    }
}
