<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Instances.ascx.cs" Inherits="Moriyama.Workflow.Umbraco6.Web.Ui.Instances" %>
<%@ Import Namespace="Moriyama.Workflow.Interfaces.Domain" %>
<%@ Import Namespace="Moriyama.Workflow.Umbraco6.Domain" %>
<%@ Import Namespace="umbraco.BusinessLogic" %>

<%@ Register src="TabRefresh.ascx" tagname="TabRefresh" tagprefix="uc1" %>

<asp:Literal ID="TrialLiteral" runat="server" Visible="false"/>

<script type="text/javascript">
    <!--
    $().ready(function () {

        var is = {};

        is.id = wutil.getTab('wfDummy');
        
        $('a.wfTransition').click(function () {
            var id = $(this).attr('rel');
            return wf.transition(id, '<%= TheGlobalisationService.GetString("transition") %>', is.id);
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
            <asp:ButtonField Text="Delete" CommandName="Delete" />
            
        </Columns>
    </asp:GridView>
</div>

<uc1:TabRefresh ID="TabRefresh1" runat="server" />


