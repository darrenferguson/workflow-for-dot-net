using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace Workflow.MVC3.WebUi.Helpers
{
    public static class ControlExtensions
    {
        public static string RenderControl(this HtmlHelper helper, Control ctl)
        {
            var sb = new StringBuilder();
            var textwriter = new StringWriter(sb);

            var htmlwriter = new HtmlTextWriter(textwriter);
            
            ctl.RenderControl(htmlwriter);

            return sb.ToString();
            
        }

        public static object ControlValue(Control ctl, string value)
        {
            if (ctl.GetType() == typeof(CheckBox) && value == "")
            {
                    return false;
            }
            else if(ctl.GetType() == typeof(CheckBox))
            {
                if (value == null) return false;
                return (value == "on" || value.ToLower() == "true");
            } else
            {
                return value;
            }
        }


        private static bool CanTransition(IWorkflowTask t)
        {
            if (!typeof(IDecisionWorkflowTask).IsAssignableFrom(t.GetType())) return false;
            return ((IDecisionWorkflowTask)t).CanTransition();
        }

        public static string TransitionInfo(this HtmlHelper helper, IWorkflowInstance i)
        {
            if (i.CurrentTask == null) return "";
            if (!typeof(IDecisionWorkflowTask).IsAssignableFrom(i.CurrentTask.GetType())) return "";

            if (!CanTransition(i.CurrentTask))
            {
                return "";
            }

            var decision = (IDecisionWorkflowTask)i.CurrentTask;
            return "<a href='" + string.Format(decision.TransitionUrl, HttpUtility.UrlEncode(i.Id.ToString())) + "'>Transition</a>";
        }
    }
}