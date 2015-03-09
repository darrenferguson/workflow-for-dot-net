<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Instances.ascx.cs" Inherits="Moriyama.Workflow.Umbraco6.Web.Workflow.Instances" %>
<%@ Import Namespace="Moriyama.Workflow.Interfaces.Domain" %>
<%@ Import Namespace="Moriyama.Workflow.Umbraco6.Domain" %>
<%@ Import Namespace="umbraco.BusinessLogic" %>

<%@ Register src="TabRefresh.ascx" tagname="TabRefresh" tagprefix="uc1" %>

<asp:Literal ID="TrialLiteral" runat="server" Visible="false"/>

<script type="text/javascript">

    var rc = {};
    $().ready(function () {
        rc.id = wutil.getTab('<%= FilterButton.ClientID %>');
     });

</script>

<script type="text/javascript">
    <!--
    $().ready(function () {

        var is = {};

        is.id = wutil.getTab('wfDummy');
        
        $('a.wfTransition').click(function () {
            var id = $(this).attr('rel');
            return wf.transition(id, '<%= TheGlobalisationService.GetString("transition") %>', is.id);
        });

        $('a.comments').click(function() {

            wf.openComments($(this).attr('rel'), '<%= TheGlobalisationService.GetString("comments") %>', rc.id);
            return false;
        });

    });
    //-->
</script>


<div id="wfDummy"></div>

<div class="workflow">
    <asp:GridView ID="WorkflowInstancesGridView" runat="server" AutoGenerateColumns="false"
        OnRowCommand="WorklowInstanceRowCommand" OnRowDeleting="WorklowInstanceRowDeleting"
        DataKeyNames="Id" OnRowDataBound="WorklowInstanceRowDataBound" CssClass="workflow">
        <Columns>
             <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <%# ((IWorkflowInstance)Container.DataItem).Name %> (<%# ((IWorkflowInstance)Container.DataItem).Id %>)
                    
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="InstantiationTime" HeaderText="Instantiation Time" />
             <asp:TemplateField HeaderText="Instantiator">
                <ItemTemplate>
                    <%# User.GetUser((((UmbracoWorkflowInstance)Container.DataItem).Instantiator)).Name %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Started" HeaderText="Running?" />
            <asp:BoundField DataField="Ended" HeaderText="Ended?" />

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
            <asp:TemplateField HeaderText="Attachments">
                <ItemTemplate>
                    <%# AttachmentInfo(((IWorkflowInstance)Container.DataItem)) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comment">
                <ItemTemplate>
                    <%# ((IWorkflowInstance)Container.DataItem).Comment %> <a href="/Workflow/Comments.aspx?id=<%# ((IWorkflowInstance)Container.DataItem).Id %>" rel="<%# ((IWorkflowInstance)Container.DataItem).Id %>" class="comments">more...</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField Text="Delete" CommandName="Delete" />
        </Columns>
        <EmptyDataTemplate><h3>No current workflows found</h3></EmptyDataTemplate>
    </asp:GridView>
</div>

<asp:Panel ID="FilterPanel" runat="server">
    
    <div style="clear: both; margin-top: 2em;">
        <asp:CheckBox ID="ShowArchivedCheckbox" runat="server" /> Display completed workflows. <asp:Button ID="FilterButton" runat="server" Text="Filter" OnClick="FilterButton_Click" />
    </div>
    
    <div style="clear:both;">
        <p>Only show workflows since:</p>
        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="SelectionChange"></asp:Calendar>
    </div>

</asp:Panel>

<uc1:TabRefresh ID="TabRefresh1" runat="server" />


