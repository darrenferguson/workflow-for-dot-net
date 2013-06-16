using System;
using System.Web.UI.WebControls;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Controls
{
    public class CriteriaOperandList : DropDownList
    {

        private string _value;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Items.Add(new ListItem("And", "and"));
            Items.Add(new ListItem("Or", "or"));

            if (_value == null) return;

            SelectedValue = _value;
        }

        public string GetValue()
        {
            return SelectedValue;
        }

        public void SetValue(string value)
        {
            _value = value;
        }
    }
}