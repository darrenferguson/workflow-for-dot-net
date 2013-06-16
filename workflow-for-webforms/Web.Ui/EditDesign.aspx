<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDesign.aspx.cs" Inherits="Web.Ui.EditDesign" %>
<%@ Import Namespace="FergusonMoriyam.Workflow.Interfaces.Ui.Adapter" %>
<html>
<head runat="server">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="Scripts/jquery-1.5.1.min.js"></script>
	<script type="text/javascript" src="Scripts/jquery-ui-1.8.12.custom.min.js"></script>

    <script type="text/javascript" src="Scripts/jquery.jsPlumb-1.3.2-all-min.js"></script>
   
    <script src="Scripts/json2.js" type="text/javascript"></script>
    <script src="Scripts/workflow.js" type="text/javascript"></script>
    <script type="text/javascript">

        wf.config = <asp:Literal runat="server" ID="WorkflowConfigLiteral"></asp:Literal>;

        wf.taskInfo = <asp:Literal runat="server" ID="TaskInfoLiteral"></asp:Literal>;
        
        wf.tasksField = '#<%= Tasks.ClientID %>';
        wf.transitionsField = '#<%= Transitions.ClientID %>';
        wf.saveButton = '#<%= SaveButton.ClientID %>';
        
        wf.guidPool =  <asp:Literal runat="server" ID="GuidPoolLiteral"></asp:Literal>;
     
        
    </script>
    
    <style type="text/css">
        html, body { height: 100%; }
        body { margin-top: 15px; }
        iframe { border-width: 0px; }
        #container
        {
            height: auto;
            min-height:100%;
            width: 100%;
        }
    </style>

</head>
<body>

<div id="container">
    <form runat="server">
    <div class="tasks">
        <asp:Repeater ID="TaskRepeater" runat="server">
            <ItemTemplate>
                <div 
                    id="<%# ((IWorkflowTaskUiAdapter) Container.DataItem).TypeName %>" 
                    class="task draggable <%# ((IWorkflowTaskUiAdapter) Container.DataItem).Class %>"
                >
                    <div class="displayEntityName">
                        <%# ((IWorkflowTaskUiAdapter) Container.DataItem).Name %>
                    </div>        
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
    </div>
    
    <div id="designer" class="designer">

        <asp:Button ID="SaveButton" runat="server" 
            onclick="SaveButtonClick" />

        <asp:Button ID="CloseWithoutSavingButton" runat="server" 
            onclick="CloseWithoutSavingButton_Click" />

        <p class="dropInfo"><asp:Literal runat="server" ID="TaskMessageLiteral"/></p>
        
        <asp:Repeater ID="TaskInstanceRepeater" runat="server">
            <ItemTemplate>
            <div 
                id="<%# ((IUiWorkflowTask) Container.DataItem).Id %>"
                class="task draggableUi <%# ((IUiWorkflowTask) Container.DataItem).Class %> design"
                style="position: absolute; top: <%# ((IUiWorkflowTask) Container.DataItem).Top %>px; left: <%# ((IUiWorkflowTask) Container.DataItem).Left %>px; z-index: 1000;"
            >
                <div class="close">x</div>
                <div class="displayEntityName"> <%# ((IUiWorkflowTask) Container.DataItem).Name %> </div>
            </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
    
    <div class="properties">
       <iframe frameBorder="0" id="propertiesFrame" src="about:blank" width="100%" height="100%"></iframe>
    </div>

    <asp:HiddenField ID="Tasks" runat="server" />
    <asp:HiddenField ID="Transitions" runat="server" />
    </form>
    </div>
</body>
</html>