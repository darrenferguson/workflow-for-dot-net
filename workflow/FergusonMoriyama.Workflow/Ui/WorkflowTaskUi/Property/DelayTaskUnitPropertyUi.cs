using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace FergusonMoriyam.Workflow.Ui.WorkflowTaskUi.Property
{
    class DelayTaskUnitPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService { get; set; }

        public DelayTaskUnitPropertyUi()
        {
            var d = new DropDownList {ID = PropertyName };

            d.Items.Add(new ListItem(TheGlobalisationService.GetString("seconds"), "seconds"));
            d.Items.Add(new ListItem(TheGlobalisationService.GetString("minutes"), "minutes"));
            d.Items.Add(new ListItem(TheGlobalisationService.GetString("hours"), "hours"));
            d.Items.Add(new ListItem(TheGlobalisationService.GetString("days"), "days"));

            RenderControl = d;   
        }
       
        public string PropertyName
        {
            get { return "Unit"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("time_unit"); }
        }

        public object Value
        {
            get { return ((DropDownList)RenderControl).SelectedValue; }
            set { ((DropDownList)RenderControl).SelectedValue = (string)value; }
        }

    }
}
