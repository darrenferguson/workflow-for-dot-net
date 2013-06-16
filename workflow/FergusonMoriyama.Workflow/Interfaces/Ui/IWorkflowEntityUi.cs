using System.Collections.Generic;
using System.Web.UI;

namespace FergusonMoriyam.Workflow.Interfaces.Ui
{
    public interface IWorkflowEntityUi
    {        
        // Type that will have it's properties available for edit in the UI
        bool SupportsType(object o);

        string EntityName { get; }

        // List of properties and details on how we render them in the UI
        IList<IWorkflowUiProperty> UiProperties { get; set;  }
        IDictionary<string, string> UiAttributes { get; set; }

        IList<Control> Render(object o);
    }
}
