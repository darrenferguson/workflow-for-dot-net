using System.Collections.Generic;
using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;
using FergusonMoriyam.Workflow.Umbraco.Ui.Controls;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Property
{
    public class UserTypePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public UserTypePropertyUi()
        {
            RenderControl = new UmbracoUserTypesList { ID = PropertyName, CssClass= "wideSelect" };
        }

        public string PropertyName
        {
            get { return "UserTypes"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("user_types"); }
        }

        public object Value
        {
            get { return ((UmbracoUserTypesList)RenderControl).GetValue(); }
            set { ((UmbracoUserTypesList)RenderControl).SetValue((List<int>)value); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}