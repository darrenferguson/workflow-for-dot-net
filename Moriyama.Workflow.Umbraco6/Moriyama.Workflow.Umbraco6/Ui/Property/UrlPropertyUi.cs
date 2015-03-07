using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;

namespace Moriyama.Workflow.Umbraco6.Ui.Property
{
    public class UrlPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public UrlPropertyUi()
        {
            RenderControl = new TextBox { ID = PropertyName };
        }

        public string PropertyName
        {
            get { return "Url"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("url"); }
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