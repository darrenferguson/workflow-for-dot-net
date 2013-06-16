<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProperties.aspx.cs" Inherits="Web.Ui.EditProperties"  MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        span { display: block; margin-bottom: 5px; }
        input { margin-bottom: 15px; }
        input.workflowTextBox { width: 400px; } 
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <p>
       <asp:Panel ID="PropertiesUiPanel" runat="server"> 
       </asp:Panel>

       <asp:Button ID="SavePropertiesButton" runat="server" Text="Save" 
        onclick="SavePropertiesButtonClick" />
    </p>
    
</asp:Content>

