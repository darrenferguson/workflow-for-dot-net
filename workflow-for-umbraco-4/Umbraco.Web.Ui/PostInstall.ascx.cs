using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using FergusonMoriyam.Workflow.Umbraco.Installer;
using FergusonMoriyam.Workflow.Umbraco.Installer.Config;
using FergusonMoriyam.Workflow.Umbraco.Installer.Database;
using Common.Logging;

using umbraco.IO;

namespace FergusonMoriyam.Workflow.Umbraco.Web.Ui
{
    public partial class PostInstall : System.Web.UI.UserControl
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected const string MinimumRequiredUmbracoVersion = "4.7.1";
        protected const string MaximumRequiredUmbracoVersion = "6.0.12";

        public Version UmbracoVersion;
        public bool UmbracoVersionCompatible;

        public DatabaseType DbType;
        public bool DatabaseCompatible;

        private string _umbracoConnectionString;

        protected override void  OnInit(EventArgs e)
        {
 	        base.OnInit(e);

            //XmlConfigurator.Configure(Helper.Instance.GetAssemblyResourceStream("Resources.Config.Log4Net.config").BaseStream);
            Log.Debug("Running installer");
            
            var version = Helper.Instance.GetWorkflowVersion();
            Log.Debug("Workflow for Umbraco version is " + version);

            WorkflowVersionLiteral.Text = version;

            UmbracoVersion = new Version(Helper.Instance.GetUmbracoVersion());

            var minVersion = new Version(MinimumRequiredUmbracoVersion);
            var maxVersion = new Version(MaximumRequiredUmbracoVersion);

            UmbracoVersionCompatible = (UmbracoVersion >= minVersion && UmbracoVersion <= maxVersion);
            Log.Debug(string.Format("Umbraco {0} is required", minVersion));
            Log.Debug(string.Format("Umbraco compatibility check: Umbraco version - '{0}'. Compatible {1}.",UmbracoVersion, UmbracoVersionCompatible));

            _umbracoConnectionString = UmbracoVersion.Major > 4
                ? ConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString
                : ConfigurationManager.AppSettings["umbracoDbDSN"];

            DbType = Helper.Instance.GetDatbaseType(_umbracoConnectionString);
            DatabaseCompatible = (DbType == DatabaseType.SqlServer || DbType == DatabaseType.MySql || DbType == DatabaseType.SqlCe);
            Log.Debug(string.Format("Database compatibility check: current version - '{0}'. Compatible {1}.", DbType, DatabaseCompatible));
            
            if(UmbracoVersionCompatible && DatabaseCompatible)
            {
                Log.Debug("Compatible: Performing install");
                PerformInstallActions();
                InstallDonePanel.Visible = true;
            } else
            {
                Log.Debug("Incomatible: Prompting user.");
                InstallerErrorPanel.Visible = true;
            }
        }

        protected void PerformInstallActions()
        {

            Log.Debug("Running SQL");

            var infrastructureConfig = IOHelper.MapPath("~/config/fmworkflow/workflow.infrastructure.spring.config");
            if (DbType == DatabaseType.SqlServer) InstallSqlServer.Instance.Run(infrastructureConfig, _umbracoConnectionString);
            if (DbType == DatabaseType.MySql) InstallMySql.Instance.Run(infrastructureConfig, _umbracoConnectionString);
            if (DbType == DatabaseType.SqlCe) InstallSqlCe.Instance.Run(infrastructureConfig, _umbracoConnectionString);
            
            Log.Debug("Running web.config install");
            InstallConfiguration.Instance.Run();

            Log.Debug("Re-configuring log4net to use workflow config");
            var logConfig = IOHelper.MapPath("~/config/fmworkflow/Log4Net.config");
            // XmlConfigurator.Configure(new FileInfo(logConfig));
        }

        protected void ManualInstallButtonClick(object sender, EventArgs e)
        {
            Log.Debug("Incompatible: User intitiated install");
            InstallerErrorPanel.Visible = false;
            PerformInstallActions();
            InstallDonePanel.Visible = true;
        }
    }
}