using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace TwitterTask.UiProperty
{
    public class AccessTokenPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        public override string PropertyName
        {
            get { return "AccessToken"; }
        }

        public string Label
        {
            get { return "Access Token"; }
        }
    }
}
