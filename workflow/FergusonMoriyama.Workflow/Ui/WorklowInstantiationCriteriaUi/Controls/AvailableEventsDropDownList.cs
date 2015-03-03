using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Application;
using log4net;

namespace FergusonMoriyam.Workflow.Ui.WorklowInstantiationCriteriaUi.Controls
{
    public class AvailableEventsDropDownList : ListBox
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IList<string> _value;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            SelectionMode = ListSelectionMode.Multiple;

            DataSource = EventInfoService.Instance.EventInformation.Events;

            DataValueField = "FullName";
            DataTextField = "FullName";

            CssClass = "evtList";

            DataBind();

            if (_value == null) return;
            foreach (var item in Items.Cast<ListItem>().Where(item => _value.Contains(item.Value)))
            {
                item.Selected = true;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);

            output.Write("<script type=\"text/javascript\">" + Environment.NewLine);
            output.Write("var fmEvt = " + new JavaScriptSerializer().Serialize(EventInfoService.Instance.EventInformation.Events) + ";" + Environment.NewLine);
            output.Write("</script>" + Environment.NewLine);
        }

        public IList<string> GetValue()
        {
            return (from ListItem listItem in Items where listItem.Selected select listItem.Value).ToList();
        }

        public void SetValue(IList<string> value)
        {
            _value = value;
        }
    }
}
