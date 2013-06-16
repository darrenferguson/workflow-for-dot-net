using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;

namespace FergusonMoriyam.Workflow.Ui.WorkflowTaskUi.Property
{
    public class HourPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {

        public IGlobalisationService TheGlobalisationService { get; set; }

        public HourPropertyUi()
        {
            var d = new DropDownList {ID = PropertyName };

            for (var i = 0; i <= 23; i++)
            {
                d.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
           
            RenderControl = d;   
        }
       
        public string PropertyName
        {
            get { return "Hour"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("hour"); }
        }

        public object Value
        {
            get { return Convert.ToInt32(((DropDownList)RenderControl).SelectedValue); }
            set { ((DropDownList)RenderControl).SelectedValue = value.ToString(); }
        }
    }
}
