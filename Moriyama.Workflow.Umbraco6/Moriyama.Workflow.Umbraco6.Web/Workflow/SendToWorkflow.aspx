<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendToWorkflow.aspx.cs"
    Inherits="Moriyama.Workflow.Umbraco6.Web.Workflow.SendToWorkflow" MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%= TheGlobalisationService.GetString("send_to_workflow") %></title>
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
            width: 90%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
   
    <div>
        <span>
            <asp:Literal ID="NoCriteriasLiteral" runat="server" Visible="false"></asp:Literal></span>
        <asp:Panel ID="SendToWorkflowPanel" runat="server" Visible="false">
            
            <span>
                <%= TheGlobalisationService.GetString("workflow_configuration") %>
            </span>
            
            <asp:DropDownList ID="AvailableCriteriaDropDownList" runat="server" />
            
            <span>
                <%= TheGlobalisationService.GetString("instantiation_comment") %>
            </span>

            <asp:TextBox ID="InstantiationCommentTextBox" runat="server" TextMode="MultiLine" CssClass="workflowTextBox"></asp:TextBox>
            
            <div style="clear: both;">
                <asp:Button ID="StartWorkflowButton" runat="server" Text="Button" OnClick="StartWorkflowButtonClick" />
            </div>

        </asp:Panel>
    </div>
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
