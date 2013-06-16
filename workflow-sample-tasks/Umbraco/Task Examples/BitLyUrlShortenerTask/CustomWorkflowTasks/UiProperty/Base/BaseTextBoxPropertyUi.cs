using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Ui;

namespace CustomWorkflowTasks.UiProperty.Base
{
    public abstract class BaseTextBoxPropertyUi : PropertyUi, IGlobalisable
    {
        public Control RenderControl { get; set; }

        protected BaseTextBoxPropertyUi()
        {
            RenderControl = new TextBox {ID = PropertyName, CssClass = "workflowTextBox"};
        }

        public abstract string PropertyName { get; }

        // The workflow designer uses this getter and setter to give or take the value of the property.
        public virtual object Value
        {
            get { return ((TextBox) RenderControl).Text; }
            set { ((TextBox) RenderControl).Text = (string) value; }
        }

        public IGlobalisationService TheGlobalisationService { get; set; }
    }
}
