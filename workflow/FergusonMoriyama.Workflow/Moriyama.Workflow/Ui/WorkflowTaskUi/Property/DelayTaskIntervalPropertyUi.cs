using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi.Property
{
    public class DelayTaskIntervalPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService { get; set; }

        public DelayTaskIntervalPropertyUi()
        {
            var d = new DropDownList {ID = PropertyName };

            for(var i =1; i <= 60; i++)
            {
                d.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            RenderControl = d;   
        }
       
        public string PropertyName
        {
            get { return "Delay"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("interval"); }
        }


        public object Value
        {
            get { return ((DropDownList)RenderControl).SelectedValue; }
            set { ((DropDownList)RenderControl).SelectedValue = Convert.ToString(value); }
        }

    
    }
}
