using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace TwitterTask.UiProperty
{
    public class TweetTextPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        public TweetTextPropertyUi() : base()
        {
            ((TextBox) RenderControl).TextMode = TextBoxMode.MultiLine;
        }

        public override string PropertyName
        {
            get { return "TweetText"; }
        }

        public string Label
        {
            get { return "Tweet Text"; }
        }
    }
}
