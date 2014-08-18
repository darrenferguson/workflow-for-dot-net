using System.Web.UI.WebControls;

namespace Moriyama.Workflow.Ui.Base
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
