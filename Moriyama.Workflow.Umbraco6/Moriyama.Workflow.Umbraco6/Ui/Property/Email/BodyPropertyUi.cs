using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi.Controls;

namespace Moriyama.Workflow.Umbraco6.Ui.Property.Email
{
    public class BodyPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public BodyPropertyUi()
        {
            RenderControl = new TextBoxMultiple { ID = PropertyName, CssClass = "workflowTextArea" };
        }

        public string PropertyName
        {
            get { return "Body"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("email_body"); }
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