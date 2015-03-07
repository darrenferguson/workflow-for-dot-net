using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Umbraco6.Ui.Controls;

namespace Moriyama.Workflow.Umbraco6.Task.Ui.Property
{
    public class MediaTypePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {

        public MediaTypePropertyUi()
        {
            RenderControl = new UmbracoMediaTypesList { ID = PropertyName, CssClass = "wideSelect" };
        }

        public string PropertyName
        {
            get { return "MediaTypes"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("media_types"); }
        }

        public object Value
        {
            get { return ((UmbracoMediaTypesList)RenderControl).GetValue(); }
            set { ((UmbracoMediaTypesList)RenderControl).SetValue((List<int>)value); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}
