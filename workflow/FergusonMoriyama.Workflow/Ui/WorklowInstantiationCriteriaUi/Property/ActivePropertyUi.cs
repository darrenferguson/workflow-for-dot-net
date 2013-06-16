using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Property
{
    public class ActivePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public ActivePropertyUi()
        {
            RenderControl = new CheckBox { ID = PropertyName };
        }
       
        public string PropertyName
        {
            get { return "Active"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("active"); }
        }

        public object Value
        {
            get { return ((CheckBox) RenderControl).Checked; }
            set { ((CheckBox) RenderControl).Checked = Convert.ToBoolean(value); }
        }
    }
}
