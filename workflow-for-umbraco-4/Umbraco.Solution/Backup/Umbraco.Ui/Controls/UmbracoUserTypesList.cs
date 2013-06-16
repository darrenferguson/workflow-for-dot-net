using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using umbraco.BusinessLogic;

namespace FergusonMoriyam.Workflow.Umbraco.Ui.Controls
{
    public class UmbracoUserTypesList : ListBox
    {
        private IList<string> _value;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            SelectionMode = ListSelectionMode.Multiple;

            DataSource = UserType.GetAllUserTypes();

            DataValueField = "Id";
            DataTextField = "Alias";

            DataBind();
            
            if (_value == null) return;

            foreach (var item in Items.Cast<ListItem>().Where(item => _value.Contains(item.Value)))
            {
                item.Selected = true;
            }
        }

        public IList<int> GetValue()
        {
            return (from ListItem listItem in Items where listItem.Selected select listItem.Value).ToList().ConvertAll(Convert.ToInt32);
        }

        public void SetValue(List<int> value)
        {
            _value = value.ConvertAll(i => i.ToString());
        }
    }
}