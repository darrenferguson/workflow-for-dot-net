using System.Reflection;
using Common.Logging;
using FergusonMoriyam.Workflow.Umbraco.Application.Modules;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Umbraco.Core;
using ApplicationEventHandler = Umbraco.Core.ApplicationEventHandler;

namespace FergusonMoriyam.Workflow.Umbraco.Application
{
    public class UmbracoWorkflowApplicationBase : ApplicationEventHandler 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private static bool _modulesRegistered;
        
        public static void RegisterModules()
        {
            if (_modulesRegistered)
                return;

            DynamicModuleUtility.RegisterModule(typeof(RegisterClientResourcesHttpModule));
            _modulesRegistered = true;
        }

        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            Log.Info(string.Format("Starting workflow for Umbraco {0}", Assembly.GetExecutingAssembly().GetName().Version));
            base.ApplicationStarting(umbracoApplication, applicationContext);
        }
    }
}
