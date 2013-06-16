using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace TwitterTask.UiProperty
{
    public class ShortUrlPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        public override string PropertyName
        {
            get { return "ShortUrlProperty"; }
        }

        public string Label
        {
            get { return "Short Url Doctype property"; }
        }
    }
}
