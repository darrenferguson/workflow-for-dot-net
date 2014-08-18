using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;

namespace Moriyama.Workflow.Umbraco6.Ui.Property.Email
{
    public class SubjectPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {

        public SubjectPropertyUi()
        {
            RenderControl = new TextBox { ID = PropertyName };
        }

        public string PropertyName
        {
            get { return "Subject"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("subject"); }
        }

        public object Value
        {
            get { return ((TextBox)RenderControl).Text; }
            set { ((TextBox)RenderControl).Text = (string)value; }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}