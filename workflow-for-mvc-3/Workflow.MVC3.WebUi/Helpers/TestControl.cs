using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Application;

namespace Workflow.MVC3.WebUi.Helpers
{
    public class TestControl : DropDownList
    {
        private int _val;

        public TestControl()
        {
           
            var configs = WorkflowConfigurationService.Instance.ListConfigurations();

            DataSource = configs;

            DataValueField = "Id";
            DataTextField = "Name";

            DataBind();

            if (Items.FindByValue(_val.ToString()) != null)
            {
                Items.FindByValue(_val.ToString()).Selected = true;
            }
        }

        public void SetValue(int i)
        {
            _val = i;
        }
    }
}