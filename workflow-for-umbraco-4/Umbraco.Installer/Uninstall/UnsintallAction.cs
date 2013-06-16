using System;
using System.Xml;
using umbraco.cms.businesslogic.packager.standardPackageActions;
using umbraco.interfaces;

namespace FergusonMoriyam.Workflow.Umbraco.Installer.Uninstall
{
    class UnsintallAction : IPackageAction
    {
        public bool Execute(string packageName, XmlNode xmlData)
        {
            throw new Exception(
                "Uninstall is not supported via the Umbraco package manager please read the Workflow product manual for details on uninstallation.");
        }

        public string Alias()
        {
            return "WorkflowUninstall";
        }

        public bool Undo(string packageName, XmlNode xmlData)
        {
            return false;
        }

        public XmlNode SampleXml()
        {
            const string sample = "<Action runat=\"uninstall\" undo=\"false\" alias=\"WorkflowUninstall\" />";
            return helper.parseStringToXmlNode(sample);
        }
    }
}
