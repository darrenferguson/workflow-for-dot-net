using System;
using System.Configuration;
using System.Data.Common;
using Moriyama.Workflow.Interfaces.Infrastructure;
using umbraco;

namespace Moriyama.Workflow.Umbraco6.Application.Infrastructure
{
    public class UmbracoConnectionStringProvider : IConnectionStringProvider
    {
        #region Instance
        private static readonly UmbracoConnectionStringProvider Provider = new UmbracoConnectionStringProvider();

        public static UmbracoConnectionStringProvider Instance
        {
            get { return Provider; }
        }

        private UmbracoConnectionStringProvider()
            : base()
        {
        }
        #endregion

        private DbConnectionStringBuilder connectionStringBuilder;

        public string GetConncetionString()
        {

            if(connectionStringBuilder == null)
            {
                var umbracoVersion = new Version(GlobalSettings.CurrentVersion);

                var umbracoConnectionString = umbracoVersion.Major > 4
                ? ConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString
                : ConfigurationManager.AppSettings["umbracoDbDSN"];

                connectionStringBuilder = new DbConnectionStringBuilder { ConnectionString = umbracoConnectionString };

                connectionStringBuilder.Remove("datalayer");
            }

            return connectionStringBuilder.ConnectionString;

        }
    }
}
