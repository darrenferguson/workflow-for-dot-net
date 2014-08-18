<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTaskProperties.aspx.cs" Inherits="Moriyama.Workflow.Umbraco6.Web.Ui.EditTaskProperties" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <title></title>
    
    <style type="text/css">
        body { background: #b6b7bc; font-size: .80em;
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }
        span { display: block; margin-bottom: 5px; }
        input { margin-bottom: 15px; }
        input.workflowTextBox { width: 200px; } 
        .submit { margin-top: 15px; }
        .workflowTextArea { width: 200px; height: 100px; }
        h2 { margin-bottom: 15px; }
        .wideSelect { width: 200px; }
    </style>

</head>
<body>
    <h2><%= TheGlobalisationService.GetString("task_properties") %></h2>
    <form id="form1" runat="server">
    
        
        <asp:Panel ID="TaskPropertiesUiPanel" runat="server">
        
        </asp:Panel>

        <asp:Button ID="SaveTaskPropertiesButton" runat="server" Text="Button" OnClick="SaveTaskPropertiesButtonClick" CssClass="submit" />
    
    </form>
    
    <script type="text/javascript">
    <!--
        var wf = {};
        wf.TaskId = '<%= HttpUtility.JavaScriptStringEncode(Request["Id"]) %>';

        <asp:Literal runat="server" ID="TaskPropertiesLiteral"></asp:Literal>

        $().ready(function() {
            if(typeof(wf.taskProps) != 'undefined') {
                window.parent.wf.setTaskProperties(wf.TaskId, wf.taskProps);
            }   
        });

    // -->
    </script>

</body>
</html>