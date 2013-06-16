<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostInstall.ascx.cs"
    Inherits="FergusonMoriyam.Workflow.Umbraco.Web.Ui.PostInstall" %>

<style type="text/css">
    table.workflowConfig { border-collapse:collapse; }
    table.workflowConfig td.heading { font-weight: bold; }
    table.workflowConfig th { background-color: #cccccc; }
    table.workflowConfig th, table.workflowConfig td { border: 1px solid #aaaaaa; padding: 5px; }
</style>

<h2>
    Workflow for Umbraco  <asp:Literal ID="WorkflowVersionLiteral" runat="server" /> - installer</h2>

<asp:Panel ID="InstallDonePanel" runat="server" Visible="false">
    <p>
        Workflow for Umbraco was succsessfully installed.
    </p>
    <h3>
        What now?
    </h3>
    <ul>
        <li>
            <a href="#" onclick="window.top.location.reload(); return false;">
                Launch workflow for Umbraco
            </a>
        </li>
        <li>
            <a href="http://vimeo.com/30190458" target="_blank">
                Watch an introductory screencast
            </a>
        </li>
        <li>
            <a href="http://our.umbraco.org/projects/backoffice-extensions/workflow" target="_blank">
                Visit the project homepage
            </a>
        </li>
        <li>
            <a href="http://our.umbraco.org/FileDownload?id=3253" target="_blank">
                Download the product manual
            </a>
        </li>
        <li>
            <a href="https://github.com/darrenferguson/workflow-for-dot-net" target="_blank">
                Get example source code
            </a>
        </li>
    </ul>

    <p>
        If you have any questions regarding Workflow for Umbraco please <a href="http://www.moriyama.co.uk/contact" target="_blank">contact Moriyama</a>.
    </p>

</asp:Panel>

<asp:Panel ID="InstallerErrorPanel" runat="server" Visible="false">
    
    <p>
        The installer found some compatibility issues.
    </p>

    <table class="workflowConfig">
        <thead>
            <tr>
                <th>
                    Attribute
                </th>
                <th>
                    Required
                </th>
                <th>
                    Your version
                </th>
                <th>
                    Compatible
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="heading">
                    Umbraco Version
                </td>
                <td>
                    <%= MinimumRequiredUmbracoVersion %> to <%= MaximumRequiredUmbracoVersion %>
                </td>
                <td>
                    <%= UmbracoVersion %>
                </td>
                <td>
                    <%= UmbracoVersionCompatible %>
                </td>
            </tr>
            <tr>
                <td class="heading">
                    Database
                </td>
                <td>
                    SqlServer or SqlCe or MySql
                </td>
                <td>
                    <%= DbType %>
                </td>
                <td>
                    <%= DatabaseCompatible %>
                </td>
            </tr>
        </tbody>
    </table>

    <p>
        If you wish to proceed with the installation anyway - please click the button below.
    </p>
    
    <asp:Button ID="ManualInstallButton" runat="server" Text="Install (I understand the risks)" onclick="ManualInstallButtonClick" />
    
</asp:Panel>