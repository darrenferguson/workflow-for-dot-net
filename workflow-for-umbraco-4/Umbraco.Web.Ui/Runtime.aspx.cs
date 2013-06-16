using System;
using System.Reflection;
using System.Web.UI;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using Common.Logging;

namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
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