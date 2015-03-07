using CustomWorkflowTasks.UiProperty.Base;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace CustomWorkflowTasks.UiProperty
{
    public class BitLyLoginPropertyUi : BaseTextBoxPropertyUi, IWorkflowUiProperty
    {
        // Public Property name of property to set in the workflow task
        public override string PropertyName
        {
            get { return "BitLyLogin"; }
        }

        // Label for control when rendered in Ui
        public string Label
        {
            get { return "Bit.ly Login"; }
        }

    }
}
