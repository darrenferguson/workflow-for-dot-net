using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;

namespace Moriyama.Workflow.Umbraco6.Ui.Property.Email
{
    public class MailInstantiatorPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public MailInstantiatorPropertyUi()
        {
            RenderControl = new CheckBox { ID = PropertyName };
        }

        public string PropertyName
        {
            get { return "MailInstantiator"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("email_instantiator"); }
        }

        public object Value
        {
            get { return ((CheckBox)RenderControl).Checked; }
            set { ((CheckBox)RenderControl).Checked = (bool)value; }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}