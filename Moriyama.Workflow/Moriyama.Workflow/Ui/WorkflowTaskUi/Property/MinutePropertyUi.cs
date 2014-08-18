using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;

namespace Moriyama.Workflow.Ui.WorkflowTaskUi.Property
{
    public class MinutePropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
         public IGlobalisationService TheGlobalisationService { get; set; }

         public MinutePropertyUi()
        {
            var d = new DropDownList {ID = PropertyName };

            for (var i = 0; i <= 59; i++)
            {
                d.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
           
            RenderControl = d;   
        }
       
        public string PropertyName
        {
            get { return "Minute"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("minute"); }
        }

        public object Value
        {
            get { return Convert.ToInt32(((DropDownList)RenderControl).SelectedValue); }
            set { ((DropDownList)RenderControl).SelectedValue = value.ToString(); }
        }
    }
}
