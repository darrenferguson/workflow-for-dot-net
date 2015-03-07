<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDesign.aspx.cs" Inherits="Moriyama.Workflow.Umbraco6.Web.Workflow.EditDesign" %>

<%@ Import Namespace="Moriyama.Workflow.Interfaces.Ui.Adapter" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <style type="text/css">
        html, body
        {
            height: 100%;
        }
        body
        {
            margin-top: 15px;
        }
        iframe
        {
            border-width: 0px;
        }
        #container
        {
            height: auto;
            min-height: 100%;
            width: 100%;
        }
    </style>
</head>
<body>
    
    <asp:Literal ID="CloseWindowLiteral" runat="server" Visible="false">
        <script type="text/javascript">
            window.close();
        </script>
    </asp:Literal>

    <script type="text/javascript">

        wf.config = <asp:Literal runat="server" ID="WorkflowConfigLiteral"></asp:Literal>;

        wf.taskInfo = <asp:Literal runat="server" ID="TaskInfoLiteral"></asp:Literal>;
        
        wf.tasksField = '#<%= Tasks.ClientID %>';
        wf.transitionsField = '#<%= Transitions.ClientID %>';
        wf.saveButton = '#<%= SaveButton.ClientID %>';
        
        wf.guidPool =  <asp:Literal runat="server" ID="GuidPoolLiteral"></asp:Literal>;
    
    </script>
    <div id="container">
        <form id="Form1" runat="server">
        <div class="tasks">
            <asp:Repeater ID="TaskRepeater" runat="server">
                <ItemTemplate>
                    <div id="<%# ((IWorkflowTaskUiAdapter) Container.DataItem).TypeName %>" class="task draggable <%# ((IWorkflowTaskUiAdapter) Container.DataItem).Class %>">
                        <div class="displayEntityName">
                            <%# ((IWorkflowTaskUiAdapter) Container.DataItem).Name %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
        <div id="designer" class="designer">
            <asp:Button ID="SaveButton" runat="server" OnClick="SaveButtonClick" />
            <asp:Button ID="CloseWithoutSavingButton" runat="server" OnClick="CloseWithoutSavingButtonClick" />
            <p class="dropInfo">
                <asp:Literal runat="server" ID="TaskMessageLiteral" /></p>
            <asp:Repeater ID="TaskInstanceRepeater" runat="server">
                <ItemTemplate>
                    <div id="<%# ((IUiWorkflowTask) Container.DataItem).Id %>" class="task draggableUi <%# ((IUiWorkflowTask) Container.DataItem).Class %> design"
                        style="position: absolute; top: <%# ((IUiWorkflowTask) Container.DataItem).Top %>px;
                        left: <%# ((IUiWorkflowTask) Container.DataItem).Left %>px; z-index: 1000;">
                        <div class="close">
                            x</div>
                        <div class="displayEntityName">
                            <%# ((IUiWorkflowTask) Container.DataItem).Name %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
        <div class="properties">
            <iframe frameborder="0" id="propertiesFrame" src="about:blank" width="100%" height="100%">
            </iframe>
        </div>

        <asp:HiddenField ID="Tasks" runat="server" />
        <asp:HiddenField ID="Transitions" runat="server" />
        </form>
    </div>
</body>
</html>
