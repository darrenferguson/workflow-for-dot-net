<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditConfigurationProperties.aspx.cs"
    Inherits="FergusonMoriyam.Workflow.Umbraco.Web.Ui.EditConfigurationProperties"
    MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master" %>

<%@ Register TagPrefix="cc1" Namespace="umbraco.uicontrols" Assembly="controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        span
        {
            display: block;
            margin-bottom: 5px;
        }
        input
        {
            margin-bottom: 15px;
        }
        input.workflowTextBox
        {
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Panel ID="PropertiesUiPanel" runat="server" />
    <asp:Button ID="SavePropertiesButton" runat="server" Text="Save" OnClick="SavePropertiesButtonClick" />
    <script type="text/javascript">
            <!--
        var tab = '<%= Request["tab"] %>';
            --> 
    </script>
    <asp:Literal ID="SavedLiteral" runat="server" Visible="false">
            <script type="text/javascript">
            <!--
                var url = wutil.addTabToUrl(UmbClientMgr.contentFrame().location.href, tab);
                UmbClientMgr.contentFrame().location.href = url;
                UmbClientMgr.closeModalWindow();
            //-->
            </script>
    </asp:Literal>
</asp:Content>
