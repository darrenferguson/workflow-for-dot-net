<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Instantiate.aspx.cs" Inherits="Web.Ui.Instantiate" MasterPageFile="~/Site.master" %>
<%@ Import Namespace="FergusonMoriyam.Workflow.Interfaces.Domain" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <p>
            <asp:Literal ID="CreatedWorkflowLiteral" runat="server" Visible="false">
            </asp:Literal>
        </p>

        <asp:GridView ID="WorkflowInstancesGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="WorklowInstanceRowCommand"
                OnRowDeleting="WorklowInstanceRowDeleting" DataKeyNames="Id" OnRowDataBound="WorklowInstanceRowDataBound">
            <Columns>           
                 <asp:BoundField DataField="Name" HeaderText="Name" />
                 <asp:BoundField DataField="InstantiationTime" HeaderText="Instantiation Time" />
                 <asp:BoundField DataField="Started" HeaderText="Running?" />   
     
                 <asp:TemplateField HeaderText="Current Task">
                    <ItemTemplate>
                        <%# ((IWorkflowInstance)Container.DataItem).CurrentTask == null ? TheGlobalisationService.GetString("n_a") : ((IWorkflowInstance)Container.DataItem).CurrentTask.Name%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transition">
                <ItemTemplate>
                    <%# TransitionInfo(((IWorkflowInstance)Container.DataItem))%>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:ButtonField Text="Delete" CommandName="Delete"/>
                <asp:ButtonField Text="Start" CommandName="Start" />

            </Columns>
        </asp:GridView>

    <asp:Button ID="RunWorkflowsButton" runat="server" onclick="RunWorkflowsButtonClick"  />

</asp:Content>