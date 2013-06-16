using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace FergusonMoriyam.Workflow.Ui.Generic
{
    class IsStartTaskPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public IsStartTaskPropertyUi()
        {
            RenderControl = new CheckBox { ID = PropertyName };
        }
       
        public string PropertyName
        {
            get { return "IsStartTask"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("is_start_task"); }
        }

        public object Value
        {
            get { return ((CheckBox) RenderControl).Checked; }
            set { ((CheckBox) RenderControl).Checked = Convert.ToBoolean(value); }
        }
    }
}
