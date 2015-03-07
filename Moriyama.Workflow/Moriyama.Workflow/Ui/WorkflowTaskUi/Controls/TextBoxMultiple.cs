using System.Web.UI.WebControls;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi.Controls
{
    public class TextBoxMultiple : TextBox
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            TextMode = TextBoxMode.MultiLine;
        }
    }
}
