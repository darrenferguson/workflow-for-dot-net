<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transition.aspx.cs" Inherits="Web.Ui.Transition" MasterPageFile="~/Site.master"%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <asp:Literal ID="CannotTransitionLiteral" runat="server" Visible="false">
        This Task cannot be transitioned.
    </asp:Literal>
    
    <asp:Panel ID="TransitionPanel" runat="server">
    <span>
       Choose Transition
    </span>
    
    <asp:DropDownList ID="TransitionDropDownList" runat="server">
    </asp:DropDownList>
    
    

    <asp:Button ID="TransitionButton" runat="server" Text="Transition" OnClick="TransitionButtonClick"/>
    </asp:Panel>
    
</asp:Content>
