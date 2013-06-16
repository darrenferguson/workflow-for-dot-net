using System;
using System.Web.UI.WebControls;
using umbraco.BusinessLogic;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Controls
{
    public class UmbracoUserListSingle : DropDownList
    {
        private int _value;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            DataSource = User.getAll();
            DataValueField = "Id";
            DataTextField = "Name";

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