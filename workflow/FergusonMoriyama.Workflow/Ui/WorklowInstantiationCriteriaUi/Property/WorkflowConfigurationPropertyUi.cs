using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Ui;
using FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Controls;

namespace FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Property
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
