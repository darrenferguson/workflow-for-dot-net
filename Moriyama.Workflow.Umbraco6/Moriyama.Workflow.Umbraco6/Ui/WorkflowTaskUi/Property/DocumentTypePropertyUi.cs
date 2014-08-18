using System.Collections.Generic;
using System.Web.UI;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Umbraco6.Ui.Controls;

namespace Moriyama.Workflow.Umbraco6.Ui.WorkflowTaskUi.Property
{
    public class DocumentTypePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {

        public DocumentTypePropertyUi()
        {
            RenderControl = new UmbracoDocumentTypesList { ID = PropertyName, CssClass = "wideSelect" };
        }

        public string PropertyName
        {
            get { return "DocumentTypes"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("document_types"); }
        }

        public object Value
        {
            get { return ((UmbracoDocumentTypesList)RenderControl).GetValue(); }
            set { ((UmbracoDocumentTypesList)RenderControl).SetValue((List<int>)value); }
        }

        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
    }
}