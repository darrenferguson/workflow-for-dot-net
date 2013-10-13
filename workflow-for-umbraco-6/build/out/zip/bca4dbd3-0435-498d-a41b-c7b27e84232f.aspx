<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transition.aspx.cs" Inherits="FergusonMoriyam.Workflow.Umbraco.Web.Ui.Transition" 
MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    
    <asp:Literal ID="CannotTransitionLiteral" runat="server" Visible="false"></asp:Literal>
    
    <asp:Panel ID="TransitionPanel" runat="server">
    <span>
        <%= TheGlobalisationService.GetString("choose_transition") %>
    </span>
    
    <asp:DropDownList ID="TransitionDropDownList" runat="server">
    </asp:DropDownList>
    
        <span>
                <%= TheGlobalisationService.GetString("transition_comment") %>
        </span>

        <asp:TextBox ID="TransitionCommentTextBox" runat="server" TextMode="MultiLine" CssClass="workflowTextBox"></asp:TextBox>
            
        <div style="clear: both;">
            <asp:Button ID="TransitionButton" runat="server" Text="Transition" OnClick="TransitionButtonClick"/>
        </div>

    </asp:Panel>
    
    

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