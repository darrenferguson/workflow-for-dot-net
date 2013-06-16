<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Criteria.aspx.cs" Inherits="Web.Ui.Criteria"
    MasterPageFile="~/Site.master" %>

<%@ Import Namespace="FergusonMoriyam.Workflow.Interfaces.Domain" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <asp:Button ID="CreateCriteriaButton" runat="server" OnClick="CreateCriteriaButtonClick" />

    <asp:GridView ID="CriteriaGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="WorklowCriteriaRowCommand"
        OnRowDeleting="WorklowCriteriaRowDeleting" DataKeyNames="Id" OnRowDataBound="WorklowCriteriaRowDataBound">

        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:ButtonField Text="Delete" CommandName="Delete" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="EditCriteria.aspx?id=<%# ((IWorkflowInstantiationCriteria) Container.DataItem).Id %>">
                        <%# TheGlobalisationService.GetString("edit_criteria") %></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
