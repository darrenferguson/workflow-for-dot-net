using System.Web.UI.WebControls;

namespace FergusonMoriyam.Workflow.Ui.Base
{
    public abstract class BaseTextBoxMultiLinePropertyUi : BaseTextBoxPropertyUi
    {
        protected BaseTextBoxMultiLinePropertyUi()
            : base()
        {
            ((TextBox) RenderControl).TextMode = TextBoxMode.MultiLine;
        }
    }
}
