using System.Collections.Generic;
using System.Web.UI;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Umbraco6.Ui.Controls;

namespace Moriyama.Workflow.Umbraco6.Ui.Property
{
    public class UserPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public UserPropertyUi()
        {
            RenderControl = new UmbracoUserList { ID = PropertyName, CssClass = "wideSelect" };
        }

        public string PropertyName
        {
            get { return "Users"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("users"); }
        }

        public object Value
        {
            get { return ((UmbracoUserList)RenderControl).GetValue(); }
            set { ((UmbracoUserList)RenderControl).SetValue((List<int>)value); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}