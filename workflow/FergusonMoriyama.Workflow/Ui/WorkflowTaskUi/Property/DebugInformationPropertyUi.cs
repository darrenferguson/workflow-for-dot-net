using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace FergusonMoriyam.Workflow.Ui.WorkflowtaskUi.Property
{
    public class DebugInformationPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService { get; set; }

        public DebugInformationPropertyUi()
        {
            RenderControl = new CheckBox {ID = PropertyName};
        }

        public string PropertyName
        {
            get { return "DebugInformation"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("debug_information"); }
        }

        public object Value
        {
            get { return ((CheckBox)RenderControl).Checked; }
            set { ((CheckBox)RenderControl).Checked = Convert.ToBoolean(value); }
        }

       
    }
}
