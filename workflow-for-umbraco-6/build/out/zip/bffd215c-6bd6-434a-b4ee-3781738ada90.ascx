<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentContent.ascx.cs" Inherits="FergusonMoriyam.Workflow.Umbraco.Web.Ui.RecentContent" %>
<%@ Import Namespace="umbraco.cms.businesslogic.web" %>

<%@ Register src="TabRefresh.ascx" tagname="TabRefresh" tagprefix="uc1" %>

<script type="text/javascript">

     var rc = {};
     $().ready(function () {
         rc.id = wutil.getTab('<%= SendToWorkflowButton.ClientID %>');
     });

</script>

<script type="text/javascript">

    $().ready(function () {

        $('#<%= SendToWorkflowButton.ClientID %>').click(function () {

            var documents = new Array();
            $('.workflowDocument:checked').each(function () {

                documents.push($(this).val());
            });

            if (documents.length > 0) {
                wf.sendToWorkflow(documents.join(','), '<%= TheGlobalisationService.GetString("send_to_workflow") %>', rc.id);
            } else {
                alert('<%= TheGlobalisationService.GetString("select_one_or_more_items") %>');
            }
        });
    });

</script>


<div class="workflow">
    <asp:Button ID="SendToWorkflowButton" runat="server" 
        Text="Send selected content to Workflow" onclick="SendToWorkflowButton_Click" />
</div>

<div class="workflow">
<asp:GridView ID="MyRecentContentGridView" runat="server" AutoGenerateColumns="false" CssClass="workflow" OnRowDataBound="RecentContentRowDataBound">

     <Columns>
        <asp:TemplateField HeaderText="Id">
            <ItemTemplate>
                 <%# ((Document) Container.DataItem).Id %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <a href="?id=<%# ((Document) Container.DataItem).Id %>">
                    <%# ((Document)Container.DataItem).Text %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Modified">
            <ItemTemplate>
                <%# ((Document)Container.DataItem).UpdateDate %> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Send to workflow?">
            <ItemTemplate>
                <input type="checkbox" id="document" value="<%# ((Document) Container.DataItem).Id %>" class="workflowDocument"/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>
</div>

<uc1:TabRefresh ID="TabRefresh1" runat="server" />
