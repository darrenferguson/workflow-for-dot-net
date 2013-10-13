using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;
using FergusonMoriyam.Workflow.Ui.WorkflowTaskUi.Controls;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Property.Email
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