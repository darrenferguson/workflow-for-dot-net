using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;
using FergusonMoriyam.Workflow.Umbraco.Ui.Controls;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Property
{
    public class TemplatePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {

        public TemplatePropertyUi()
        {
            RenderControl = new UmbracoTemplateList { ID = PropertyName, CssClass = "wideSelect" };
        }

        public string PropertyName
        {
            get { return "RenderTemplate"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("template"); }
        }

        public object Value
        {
            get { return ((UmbracoTemplateList)RenderControl).GetValue(); }
            set { ((UmbracoTemplateList)RenderControl).SetValue((int)value); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}