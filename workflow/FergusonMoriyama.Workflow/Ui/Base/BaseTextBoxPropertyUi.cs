using System.Web.UI.WebControls;

namespace FergusonMoriyam.Workflow.Ui.Base
{
    public abstract class BaseTextBoxPropertyUi : BasePropertyUi
    {
        
        protected BaseTextBoxPropertyUi() : base()
        {
            RenderControl = new TextBox { ID = PropertyName, CssClass = "workflowTextBox" };
        }
        
        // The workflow designer uses this getter and setter to give or take the value of the property.
        public virtual object Value
        {
            get { return ((TextBox)RenderControl).Text; }
            set { ((TextBox)RenderControl).Text = (string)value; }
        }

    }
}
