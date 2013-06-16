<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCriteria.aspx.cs" Inherits="Web.Ui.EditCriteria" MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        span { display: block; margin-bottom: 5px; }
        input { margin-bottom: 15px; }
        input.workflowTextBox { width: 400px; } 
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <p>
       <asp:Panel ID="CiteriaControlsPanel" runat="server"/> 

       <asp:Button ID="SaveCriteriaButton" runat="server" onclick="SaveCriteriaButtonClick" />
    </p>
</asp:Content>
