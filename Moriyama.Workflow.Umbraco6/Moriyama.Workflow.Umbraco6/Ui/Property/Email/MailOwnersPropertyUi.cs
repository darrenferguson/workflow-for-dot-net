using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;

namespace Moriyama.Workflow.Umbraco6.Ui.Property.Email
{
    public class MailOwnersPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {

        public MailOwnersPropertyUi()
        {
            RenderControl = new CheckBox { ID = PropertyName };
        }

        public string PropertyName
        {
            get { return "MailNodeOwners"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("email_owners"); }
        }

        public object Value
        {
            get { return ((CheckBox)RenderControl).Checked; }
            set { ((CheckBox)RenderControl).Checked = (bool)value; }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}