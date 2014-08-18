using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;

namespace Moriyama.Workflow.Ui.WorkflowConfigurationUi
{
    public class IsConfigurationActivePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public IsConfigurationActivePropertyUi()
        {
            RenderControl = new CheckBox {ID = PropertyName};
        }

        public string PropertyName
        {
            get { return "IsConfigurationActive"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("is_configuration_active"); }
        }

        public object Value
        {
            get { return ((CheckBox)RenderControl).Checked; }
            set { ((CheckBox)RenderControl).Checked = Convert.ToBoolean(value); }
        }

       
    }
}
