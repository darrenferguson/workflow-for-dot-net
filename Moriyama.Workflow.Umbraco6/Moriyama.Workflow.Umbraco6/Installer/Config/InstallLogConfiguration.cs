using System.Xml;

namespace Moriyama.Workflow.Umbraco6.Installer.Config
{
    public class InstallLogConfiguration : BaseInstallConfiguration
    {

        #region instance
        private static readonly InstallLogConfiguration Installer = new InstallLogConfiguration();
        public static InstallLogConfiguration Instance { get { return Installer; } }
        private InstallLogConfiguration() { }
        #endregion
        
        public override XmlDocument Run(XmlDocument configDocument)
        {
            Log.Debug("Installing Log4net config");

            configDocument = AddConfig(
                configDocument,
                "section[@name='log4net']",
                "//configuration/configSections",
                Helper.Instance.ReadAssemblyResource("Resources.Config.Web.Section.Log4Net.config"));

            Log.Debug("Finished Installing Log4net config");

            return configDocument;

        }
    }
}
