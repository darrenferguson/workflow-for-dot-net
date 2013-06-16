using System;
using System.IO;
using System.Reflection;
using System.Web;
using log4net;
using umbraco.IO;

namespace FergusonMoriyam.Workflow.Umbraco.Installer.Config
{
    public class InstallConfiguration : Singleton<InstallConfiguration>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Run()
        {
            Log.Debug("Installing workflow for Umbraco configuration");
            
            var config = IOHelper.MapPath(VirtualPathUtility.ToAbsolute("~/web.config"));
            var backupConfigFile = IOHelper.MapPath(VirtualPathUtility.ToAbsolute("~/"+DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".workflow.backup.web.config"));

            File.Copy(config, backupConfigFile);
            Log.Debug("Backed up web.config to " + backupConfigFile);

            Log.Debug("Reading " + config);

            var configDocument = umbraco.xmlHelper.OpenAsXmlDocument(config);

            configDocument = InstallLogConfiguration.Instance.Run(configDocument);
            configDocument = InstallSpringConfiguration.Instance.Run(configDocument);
            
            Log.Debug("Writing " + config);
            configDocument.Save(config);

            var dashboardConfigFile = IOHelper.MapPath(VirtualPathUtility.ToAbsolute("~/config/Dashboard.config"));
            var dashboardConfigDocument = umbraco.xmlHelper.OpenAsXmlDocument(dashboardConfigFile);

            dashboardConfigDocument = InstallDashboardConfiguration.Instance.Run(dashboardConfigDocument);

            dashboardConfigDocument.Save(dashboardConfigFile);



            Log.Debug("Exiting config installer");
        }
    }
}
