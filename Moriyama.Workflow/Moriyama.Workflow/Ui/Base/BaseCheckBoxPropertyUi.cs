using System;
using System.Web.UI.WebControls;

namespace Moriyama.Workflow.Ui.Base
{
    public abstract class BaseCheckBoxPropertyUi : BasePropertyUi
    {
        protected BaseCheckBoxPropertyUi() : base()
        {
            RenderControl = new CheckBox { ID = PropertyName };
        }

        public object Value
        {
            get { return ((CheckBox)RenderControl).Checked; }
            set { ((CheckBox)RenderControl).Checked = Convert.ToBoolean(value); }
        }
    }
}
