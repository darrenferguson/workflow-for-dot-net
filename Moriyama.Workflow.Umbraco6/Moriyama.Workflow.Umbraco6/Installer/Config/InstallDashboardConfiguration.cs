using System.Xml;

namespace Moriyama.Workflow.Umbraco6.Installer.Config
{

    public class InstallDashboardConfiguration : BaseInstallConfiguration
    {

        #region instance
    private static readonly InstallDashboardConfiguration Installer = new InstallDashboardConfiguration();
    public static InstallDashboardConfiguration Instance { get { return Installer; } }
    private InstallDashboardConfiguration() { }
    #endregion

        public override XmlDocument Run(XmlDocument configDocument)
        {
            Log.Debug("Installing Dashboard config");

            configDocument = AddConfig(
                configDocument,
                "section[@alias='Workflow Content']",
                "//dashBoard",
                Helper.Instance.ReadAssemblyResource("Resources.Config.Dashboard.Content.Dashboard.config"));

            configDocument = AddConfig(
               configDocument,
               "section[@alias='Workflow Developer']",
               "//dashBoard",
               Helper.Instance.ReadAssemblyResource("Resources.Config.Dashboard.Developer.Dashboard.config"));

            Log.Debug("Finished Installing Dashboard config");

            return configDocument;
        }
    }
}
