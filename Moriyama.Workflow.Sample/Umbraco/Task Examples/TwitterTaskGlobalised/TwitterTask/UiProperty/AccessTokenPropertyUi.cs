using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace TwitterTask.UiProperty
{
    public class AccessTokenPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public override string PropertyName
        {
            get { return "AccessToken"; }
        }

        public string Label
        {
            get { return TheGlobalisationService.GetString("access_token"); }
        }
    }
}
