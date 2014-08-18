<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Configuration.ascx.cs"
    Inherits="Moriyama.Workflow.Umbraco6.Web.Workflow.Configuration" %>
<%@ Import Namespace="Moriyama.Workflow.Interfaces.Domain" %>
<%@ Register src="TabRefresh.ascx" tagname="TabRefresh" tagprefix="uc1" %>



<asp:Literal ID="TrialLiteral" runat="server" Visible="false"/>

<div class="workflow">
    <asp:DropDownList ID="WorkflowConfigsDropDownList" runat="server"/>
    <asp:Button ID="CreateButton" runat="server" OnClick="CreateButtonClick" />
</div>

<script type="text/javascript">
     <!--
     var cp = {};
     $().ready(function () {
         cp.id = wutil.getTab('<%= CreateButton.ClientID %>');
     });
     // -->
</script>


<div class="workflow">
    <asp:GridView ID="WorkflowConfigGridView" runat="server" AutoGenerateColumns="false"
        OnRowCommand="WorklowConfigsRowCommand" OnRowDeleting="WorklowConfigsRowDeleting"
        OnRowDataBound="WorklowConfigsRowDataBound" DataKeyNames="Id" CssClass="workflow">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Description" />
            <asp:TemplateField>
                <ItemTemplate>
                    <%# ((IWorkflowConfiguration)Container.DataItem).TypeName.Substring(((IWorkflowConfiguration)Container.DataItem).TypeName.LastIndexOf(".")+1)%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IsConfigurationActive" HeaderText="Active?" />
            <asp:ButtonField Text="Delete" CommandName="Delete" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="#" onclick="return wf.editProperties('<%# ((IWorkflowConfiguration) Container.DataItem).Id %>', '<%# TheGlobalisationService.GetString("edit_properties") %>', cp.id);">
                        <%# TheGlobalisationService.GetString("edit_properties") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="plugins/fmworkflow/EditDesign.aspx?id=<%# ((IWorkflowConfiguration) Container.DataItem).Id %>" target="_blank">
                        <%# TheGlobalisationService.GetString("edit_design") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<uc1:TabRefresh ID="TabRefresh1" runat="server" />

