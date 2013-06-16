using System.Web.UI;

namespace FergusonMoriyam.Workflow.Interfaces.Ui
{
    public interface IWorkflowUiProperty
    {
        // Property to render in the UI
        string PropertyName { get; }

        // Control to use 
        Control RenderControl { get; }

        // Label - should be localisation aware
        string Label { get; }

        // Property of the RenderControl used to retrieve its value on postback.
        // string RenderControlValueProperty { get; }

        object Value { get; set; }
    }
}
