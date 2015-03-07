using System.Reflection;
using log4net;
using Moriyama.Workflow.Umbraco6.Application.Modules;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using umbraco.BusinessLogic;
using umbraco.businesslogic;
using Umbraco.Core;


namespace Moriyama.Workflow.Umbraco6.Application
{
    public class UmbracoWorkflowApplicationBase : IApplicationEventHandler 
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

        
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            Log.Info(string.Format("Starting workflow for Umbraco {0}", Assembly.GetExecutingAssembly().GetName().Version));
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            
        }
    }
}
