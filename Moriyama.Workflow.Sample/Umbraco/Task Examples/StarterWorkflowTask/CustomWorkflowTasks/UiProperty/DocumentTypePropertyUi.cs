using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;

namespace CustomWorkflowTasks.UiProperty
{
    public class DocumentTypePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public DocumentTypePropertyUi()
        {
            // Just says that a TextBox will be used to get the property value from the user.
            RenderControl = new TextBox { ID = PropertyName, CssClass = "workflowTextBox" };
        }

        // Public Property name of property to set in the workflow task
        public string PropertyName
        {
            get { return "DocumentTypeProperty"; }
        }

        public Control RenderControl { get; private set; }

        // Label for control when rendered in Ui
        public string Label
        {
            get { return "Doc Type Property"; }
        }

        // The workflow designer uses this getter and setter to give or take the value of the property.
        public object Value
        {
            get { return ((TextBox)RenderControl).Text; }
            set { ((TextBox)RenderControl).Text = (string)value; }
        }

        // If a class implements IGlobalisable the workflow runtime will pass an IGlobalisationService so you can
        // use the appropriate language when returning strings. it isn't used in this sample.
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}
