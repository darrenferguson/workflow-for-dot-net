<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomTransition.aspx.cs" 
    Inherits="CustomWorkflowDecisionTask.CustomTransition" MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
 
    <div>
        <asp:Literal ID="CannotTransitionLiteral" runat="server" Visible="false"></asp:Literal>

        <div>
            <asp:DropDownList ID="TransitonDropDownList" runat="server">
            </asp:DropDownList>
        </div>

        <div style="margin-top: 15px;">
            Transition Comment:<br />
            <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" />
        </div>

        <div>
            <asp:Button ID="TransitionButon" runat="server" Text="Transition the workflow"  OnClick="TransitionButtonClick"/>
        </div>

    </div>

    <script type="text/javascript">
            <!--
        var tab = '<%= Request["tab"] %>';
            --> 
    </script>

    <!-- This just closes the dialogue and refreshes when shown -->
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
