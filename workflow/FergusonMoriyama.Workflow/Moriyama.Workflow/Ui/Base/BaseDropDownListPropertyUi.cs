using System.Web.UI.WebControls;

namespace Moriyama.Workflow.Ui.Base
{
    public abstract class BaseDropDownListPropertyUi : BasePropertyUi
    {
        protected BaseDropDownListPropertyUi() : base()
        {
            RenderControl = new DropDownList { ID = PropertyName, CssClass = "workflowTextBox" };
        }

        public object Value
        {
            get { return ((DropDownList)RenderControl).SelectedValue; }
            set { ((DropDownList)RenderControl).SelectedValue = (string)value; }
        }
    }
}
