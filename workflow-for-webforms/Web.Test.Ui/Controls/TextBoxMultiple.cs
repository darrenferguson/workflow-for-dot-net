using System.Web.UI.WebControls;

namespace Web.Test.Ui.Controls
{
    class TextBoxMultiple : TextBox
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            TextMode = TextBoxMode.MultiLine;
        }
    }
}
