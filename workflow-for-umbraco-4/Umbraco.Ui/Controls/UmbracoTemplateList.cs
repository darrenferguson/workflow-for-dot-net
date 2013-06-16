using System;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.template;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Controls
{
    public class UmbracoTemplateList : DropDownList
    {
        private int _value;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DataSource = Template.GetAllAsList();

            DataValueField = "Id";
            DataTextField = "Alias";

            DataBind();
            
            SelectedValue = _value.ToString();
        }

        public int GetValue()
        {
            return Convert.ToInt32(SelectedValue);
        }

        public void SetValue(int value)
        {
            _value = value;
        }
    }
}