<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InstantiationCriteria.ascx.cs"
    Inherits="Moriyama.Workflow.Umbraco6.Web.Ui.InstantiationCriteria" %>
<%@ Import Namespace="Moriyama.Workflow.Interfaces.Domain" %>

<%@ Register src="TabRefresh.ascx" tagname="TabRefresh" tagprefix="uc1" %>


<asp:Literal ID="TrialLiteral" runat="server" Visible="false"/>

<div class="workflow">
    <asp:Button ID="CreateCriteriaButton" runat="server" OnClick="CreateCriteriaButtonClick" />
</div>

<script type="text/javascript">
     <!--
     var pg = {};
     $().ready(function () {
         pg.id = wutil.getTab('<%= CreateCriteriaButton.ClientID %>');
     });
     // -->
</script>

<div class="workflow">

    <asp:GridView ID="CriteriaGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="WorklowCriteriaRowCommand"
        OnRowDeleting="WorklowCriteriaRowDeleting" DataKeyNames="Id" OnRowDataBound="WorklowCriteriaRowDataBound" CssClass="workflow">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:ButtonField Text="Delete" CommandName="Delete" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="#" onclick="return wf.editCriteria('<%# ((IWorkflowInstantiationCriteria) Container.DataItem).Id %>', '<%# TheGlobalisationService.GetString("edit_criteria") %>', pg.id);">
                        <%# TheGlobalisationService.GetString("edit_criteria") %></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>


<uc1:TabRefresh ID="TabRefresh1" runat="server" />



