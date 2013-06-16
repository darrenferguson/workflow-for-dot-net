<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web.Ui._Default" %>

<%@ Import Namespace="FergusonMoriyam.Workflow.Interfaces.Domain" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        <%# TheGlobalisationService.GetString("workflow") %>
    </h2>

    <asp:DropDownList ID="WorkflowConfigsDropDownList" runat="server">
    </asp:DropDownList>

    <asp:Button ID="CreateButton" runat="server" OnClick="CreateButtonClick" />

    <asp:GridView ID="WorkflowConfigGridView" runat="server" AutoGenerateColumns="false" 
        OnRowCommand="WorklowConfigsRowCommand" 
        OnRowDeleting="WorklowConfigsRowDeleting" 
        OnRowDataBound="WorklowConfigsRowDataBound" DataKeyNames="Id">
        <Columns>

            <asp:BoundField DataField="Name" HeaderText="Description" />
            <asp:BoundField DataField="TypeName" HeaderText="Type" />
            <asp:BoundField DataField="IsConfigurationActive" HeaderText="Active?" />
            <asp:BoundField DataField="IsLocked" HeaderText="Locked?" />
            
            <asp:ButtonField Text="Delete" CommandName="Delete" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="EditProperties.aspx?id=<%# ((IWorkflowConfiguration) Container.DataItem).Id %>">
                        <%# TheGlobalisationService.GetString("edit_properties") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="EditDesign.aspx?id=<%# ((IWorkflowConfiguration) Container.DataItem).Id %>">
                        <%# TheGlobalisationService.GetString("edit_design") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="Instantiate.aspx?id=<%# ((IWorkflowConfiguration) Container.DataItem).Id %>">
                        <%# TheGlobalisationService.GetString("instantiate") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    
</asp:Content>
