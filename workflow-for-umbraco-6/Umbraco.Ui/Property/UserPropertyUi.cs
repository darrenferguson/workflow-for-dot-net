using System.Collections.Generic;
using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;
using FergusonMoriyam.Workflow.Umbraco.Ui.Controls;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Property
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