using System.Reflection;
using Common.Logging;
using FergusonMoriyam.Workflow.Interfaces.Application.Event;
using FergusonMoriyam.Workflow.Umbraco.Application.Modules;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Spring.Context.Support;
using Umbraco.Core;
using Umbraco.Web;
using umbraco.BusinessLogic;
using umbraco.cms.presentation.Trees;

namespace FergusonMoriyam.Workflow.Umbraco.Application
{
    public class UmbracoWorkflowApplicationBase : ApplicationBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static bool _modulesRegistered;

        public UmbracoWorkflowApplicationBase()
        {
            Log.Info(string.Format("Starting workflow for Umbraco {0}", Assembly.GetExecutingAssembly().GetName().Version));

            var ctx = ContextRegistry.GetContext();
            var eventService = (IEventService)ctx.GetObject("EventService");
            

            Log.Debug("Registering events");
            eventService.RegisterEvents();

            Log.Debug("Registering custom menu items");
            BaseContentTree.BeforeNodeRender += BaseContentTreeBeforeNodeRender;
        }


        
        void BaseContentTreeBeforeNodeRender(ref XmlTree sender, ref XmlTreeNode node, System.EventArgs e)
        {
            Log.Debug(string.Format("Rendering Tree " + sender.GetType()));
        }

        public static void RegisterModules()
        {
            if (_modulesRegistered)
                return;

            DynamicModuleUtility.RegisterModule(typeof(RegisterClientResourcesHttpModule));
            _modulesRegistered = true;
        }

       
    }
}
