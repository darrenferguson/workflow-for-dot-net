using System.Web.UI.WebControls;

namespace FergusonMoriyam.Workflow.Ui.WorkflowTaskUi.Controls
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
