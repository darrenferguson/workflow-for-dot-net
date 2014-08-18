using System.Web.UI;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorkflowTaskUi.Controls;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi.Property
{
    class TaskDescriptionPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService { get; set; }

        public TaskDescriptionPropertyUi()
        {
            RenderControl = new TextBoxMultiple {ID = PropertyName, CssClass = "workflowTextArea"};
        }
       
        public string PropertyName
        {
            get { return "Description"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("description"); }
        }
    
        public object Value
        {
            get { return ((TextBoxMultiple)RenderControl).Text; }
            set { ((TextBoxMultiple)RenderControl).Text = (string)value; }
        }
    }
}
