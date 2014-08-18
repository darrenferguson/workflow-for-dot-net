using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;

namespace Moriyama.Workflow.Ui.Generic
{
    public class NamePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public NamePropertyUi()
        {
            RenderControl = new TextBox {ID = PropertyName, CssClass = "workflowTextBox"};
        }
        
        public string PropertyName
        {
            get { return "Name"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("name"); }
        }

        public object Value
        {
            get { return ((TextBox) RenderControl).Text; }
            set { ((TextBox)RenderControl).Text = (string) value; }
        }

        
    }
}
