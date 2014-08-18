using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui.WorklowInstantiationCriteriaUi.Controls;

namespace Moriyama.Workflow.Ui.WorklowInstantiationCriteriaUi.Property
{
    public class WorkflowConfigurationPropertyUi : PropertyUi, IWorkflowUiProperty, IGlobalisable
    {
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }

        public WorkflowConfigurationPropertyUi()
        {
            RenderControl = new WorkflowConfigurationDropDownList { ID = PropertyName };
        }
       
        public string PropertyName
        {
            get { return "WorkflowConfiguration"; }
        }

        public Control RenderControl { get; private set; }

        public string Label
        {
            get { return TheGlobalisationService.GetString("workflow_configuration"); }
        }


        public object Value
        {
            get { return Convert.ToString(((WorkflowConfigurationDropDownList)RenderControl).SelectedValue); }
            set
            {
                if (Convert.ToInt32(value) == 0) return;
                ((WorkflowConfigurationDropDownList)RenderControl).SetValue(Convert.ToInt32(value));
            }
        }
    }
}
