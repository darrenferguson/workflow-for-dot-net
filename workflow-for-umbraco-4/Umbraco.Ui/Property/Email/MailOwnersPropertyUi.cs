using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Property.Email
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