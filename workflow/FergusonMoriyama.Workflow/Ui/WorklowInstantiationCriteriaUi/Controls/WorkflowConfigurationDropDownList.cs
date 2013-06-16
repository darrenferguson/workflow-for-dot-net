using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Application;

namespace FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Controls
{
    public class WorkflowConfigurationDropDownList : DropDownList
    {
        private int _val;

        public WorkflowConfigurationDropDownList()
        {

            var configs = WorkflowConfigurationService.Instance.ListConfigurations();
            foreach(var config in configs)
            {
                Items.Add(new ListItem(config.Name, config.Id.ToString()));
            }
        }
        
        public void SetValue(int i)
        {
            _val = i;
            if (Items.FindByValue(_val.ToString()) != null)
            {
                Items.FindByValue(_val.ToString()).Selected = true;
            }
        }
    }
}
