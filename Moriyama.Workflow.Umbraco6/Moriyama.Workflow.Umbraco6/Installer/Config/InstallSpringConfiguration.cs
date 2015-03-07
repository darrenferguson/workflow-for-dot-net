using System;
using System.Xml;

namespace Moriyama.Workflow.Umbraco6.Installer.Config
{
    public class InstallSpringConfiguration : BaseInstallConfiguration
    {
        #region instance
        private static readonly InstallSpringConfiguration Installer = new InstallSpringConfiguration();
        public static InstallSpringConfiguration Instance { get { return Installer; } }
        private InstallSpringConfiguration() { }
        #endregion

        public override XmlDocument Run(XmlDocument configDocument)
        {
            Log.Debug("Installing Spring config");
            
            configDocument = AddConfig(
                configDocument,
                "sectionGroup[@name='spring']",
                "//configuration/configSections",
                Helper.Instance.ReadAssemblyResource("Resources.Config.Web.SectionGroup.Spring.config"));


            configDocument = AddConfig(
               configDocument,
               "spring",
               "//configuration",
               Helper.Instance.ReadAssemblyResource("Resources.Config.Web.Spring.Context.config"));

            configDocument = AddConfig(
               configDocument,
               "add[@name='Spring']",
               "//configuration/system.web/httpModules",
               Helper.Instance.ReadAssemblyResource("Resources.Config.Web.Spring.Context.Support.WebSupportModule.config"));

            configDocument = AddConfig(
              configDocument,
              "add[@name='Spring']",
              "//configuration/system.webServer/modules",
              Helper.Instance.ReadAssemblyResource("Resources.Config.Web.Spring.Context.Support.WebSupportModule.config"));


           configDocument = AddConfig(
            configDocument,
            "add[@name='SpringPageHandler']",
            "//configuration/system.web/httpHandlers",
            Helper.Instance.ReadAssemblyResource("Resources.Config.Web.Spring.Web.Support.PageHandlerFactory.config"));

           configDocument = AddConfig(
            configDocument,
            "add[@name='SpringContextMonitor']",
            "//configuration/system.webServer/handlers",
            Helper.Instance.ReadAssemblyResource("Resources.Config.Web.Spring.Web.Support.ContextMonitor.config"));

           configDocument = AddConfig(
            configDocument,
            "add[@name='SpringPageHandler']",
            "//configuration/system.webServer/handlers",
            Helper.Instance.ReadAssemblyResource("Resources.Config.Web.Spring.Web.Support.PageHandlerFactory.config"));
           
           Log.Debug("Finished Installing Spring config");
           return configDocument;
        }
    }
}
