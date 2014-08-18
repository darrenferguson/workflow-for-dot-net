using System.Web.UI;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Umbraco6.Ui.Controls;

namespace Moriyama.Workflow.Umbraco6.Ui.Property.Email
{
    public class FromPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public FromPropertyUi()
        {
            RenderControl = new UmbracoUserListSingle { ID = PropertyName };
        }

        public string PropertyName
        {
            get { return "From"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("from"); }
        }

        public object Value
        {
            get { return ((UmbracoUserListSingle)RenderControl).GetValue(); }
            set { ((UmbracoUserListSingle)RenderControl).SetValue((int)value); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}