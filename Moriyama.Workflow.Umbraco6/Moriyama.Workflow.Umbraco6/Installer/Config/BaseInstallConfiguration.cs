using System.Reflection;
using System.Xml;
using Common.Logging;

namespace Moriyama.Workflow.Umbraco6.Installer.Config
{
    public abstract class BaseInstallConfiguration
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public abstract XmlDocument Run(XmlDocument configDocument);

        protected XmlDocument AddConfig(XmlDocument configDocument, string checkXPath, string insertXPath, string fragment)
        {
            Log.Debug("Adding config: " + fragment);
            var searchXpath = insertXPath + "/" + checkXPath;

            var node = configDocument.SelectSingleNode(searchXpath);

            if (node == null)
            {
                Log.Debug("Inserting config");
                node = configDocument.SelectSingleNode(insertXPath);
                var fragmentNode = GetFragment(fragment);
                node.AppendChild(configDocument.ImportNode(fragmentNode, true));
            }
            else
            {
                Log.Debug("Config already exists");
            }

            return configDocument;
        }

        protected XmlNode GetFragment(string s)
        {
            Log.Debug("Creating XMl Fragment: " + s);
            var d = new XmlDocument();
            d.LoadXml("<xml>" + s + "</xml>");
            return d.DocumentElement.FirstChild;
        }
    }
}
