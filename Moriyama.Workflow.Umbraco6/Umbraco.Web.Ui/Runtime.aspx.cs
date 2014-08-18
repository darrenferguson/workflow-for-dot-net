using System;
using System.Reflection;
using System.Web.UI;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Common.Logging;

namespace Moriyama.Workflow.Umbraco6.Web.Ui
{
    public partial class Runtime : Page
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IWorkflowRuntime TheWorkflowRuntime { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Debug("Processing workflow runtime");

            TheWorkflowRuntime.RunWorkflows();

            Log.Debug("Done processing workflows");

        }
    }
}