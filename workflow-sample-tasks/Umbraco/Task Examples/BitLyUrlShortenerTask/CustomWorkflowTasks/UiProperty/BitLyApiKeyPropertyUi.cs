using CustomWorkflowTasks.UiProperty.Base;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace CustomWorkflowTasks.UiProperty
{
    public class BitLyApiKeyPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        // Public Property name of property to set in the workflow task
        public override string PropertyName
        {
            get { return "BitLyApiKey"; }
        }

        // Label for control when rendered in Ui
        public string Label
        {
            get { return "Bit.ly API key"; }
        }
    }
}
