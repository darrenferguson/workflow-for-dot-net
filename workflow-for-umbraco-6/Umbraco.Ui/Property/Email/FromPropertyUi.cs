using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui;
using FergusonMoriyam.Workflow.Umbraco.Ui.Controls;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Property.Email
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