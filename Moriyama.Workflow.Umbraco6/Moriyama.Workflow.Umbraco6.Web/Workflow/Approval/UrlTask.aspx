<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UrlTask.aspx.cs" Inherits="Moriyama.Workflow.Umbraco6.Web.Workflow.Approval.UrlTask" %>
<!doctype html>
<html class="no-js" lang="en">
  <head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Workflow Approval</title>
    <link rel="stylesheet" href="css/foundation.css" />
    <link rel="stylesheet" href="css/foundation-icons.css" />
    <link rel="stylesheet" href="css/styles.css" />
    <asp:Panel runat="server" ID="ClosePanel" Visible="False">
        <script type="text/javascript">
            window.opener.location = window.opener.location;
            window.close();
        </script>
     </asp:Panel>
     <script type="text/javascript">
         var wf = {};
         wf.activeItem = null;
         wf.nodeDetails = <asp:Literal runat="server" ID="JsonLiteral"></asp:Literal> ;
     </script>
    <script src="js/vendor/modernizr.js"></script>
  </head>
  <body>

    <div class="row">
      <div class="large-3 medium-3 columns approval-pane">
          <h3>Workflow Approval</h3>
          <p>To approve a page, choose one of the pages from below and then choose either accept or reject. </p>

        <ul class="side-nav">
        
        </ul>

        <div class="radio hide">
         <input type="radio" name="decision" value="Accept" id="acceptWorkflow"><label for="Accept">Accept</label>
         <input type="radio" name="decision" value="Reject" id="rejectWorkflow"><label for="Reject">Reject</label>
       </div>

       <form id="rejectionReason" class="decisions hide">
         <div class="rejectUi">
         <label>Reason for rejection</label><br />
         <textarea placeholder="Reason for rejection" class="rejectArea"></textarea>
       <%-- <div class="decision-btn">
          <a href="#" class="small success button">OK</a>
          <a href="#" class="small alert button">Cancel</a>
        </div>--%>
        </div>
        </form>
        <div class="accept-btn">
          <a href="#" class="small button expand">Submit Approvals</a>
        </div>
      </div>

      <div class="large-9 medium-9 columns approval-section">
      
        <div id="pages-for-approval">
            
          <asp:Repeater id="NodeRepeater" runat="server">
            <ItemTemplate>
                <div class="page" data-name="<%# Eval("Name")%>" data-id="<%# Eval("Id")%>" data-url="<%# Eval("Url")%>" data-refer="<%# Eval("References")%>">
                    <iframe src="/umbraco/dialogs/preview.aspx?id=<%# Eval("PreviewNodeId")%>"></iframe>
                </div>
            </ItemTemplate>
          </asp:Repeater>

        </div>

        </div>        
    </div>
    
    <form runat="server" class="hiddenForm" style="display: none;">
        <asp:HiddenField ID="JsonField" runat="server" />
        <asp:Button ID="JsonButton" runat="server" Text="Button" OnClick="BtnClick"/>
    </form>
    
    <script type="text/javascript">
        <!-- 
        wf.buttonId = '#<%= JsonButton.ClientID %>'; 
        wf.fieldId = '#<%= JsonField.ClientID %>'; 
        // -->
    </script>

    <script src="js/vendor/jquery.js"></script>
    <script src="js/foundation.min.js"></script>
    <script>
        $(document).foundation();
    </script>
    <script src="js/approvalFrames.js"></script>
    <script src="js/sideBar.js"></script>
    
    <script src="js/decisionBtn.js"></script>

  </body>
</html>
