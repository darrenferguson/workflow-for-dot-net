<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInstantiationCriteria.aspx.cs"
    Inherits="FergusonMoriyam.Workflow.Umbraco.Web.Ui.EditInstantiationCriteria"
    MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        span
        {
            display: block;
            margin-bottom: 5px;
        }
        input, select, textarea
        {
            margin-bottom: 15px;
        }
        input.workflowTextBox
        {
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
    <asp:Panel ID="CiteriaControlsPanel" runat="server" />
    <asp:Button ID="SaveCriteriaButton" runat="server" OnClick="SaveCriteriaButtonClick" />
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
