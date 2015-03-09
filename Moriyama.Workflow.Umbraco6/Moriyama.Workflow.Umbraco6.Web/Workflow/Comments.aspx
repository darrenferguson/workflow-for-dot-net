<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="Moriyama.Workflow.Umbraco6.Web.Workflow.Comments" 
    MasterPageFile="~/umbraco/masterpages/umbracoDialog.Master" %>

<%@ Import Namespace="umbraco.BusinessLogic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Comments</title>
     <style type="text/css">
        span
        {
            display: block;
            margin-bottom: 5px;
        }
        input, select, textarea
        {
            margin-bottom: 15px;
        }
        input.workflowTextBox
        {
            width: 90%;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div>
        <h2>Instance <%= UmbracoWorkflowInstance.Id %> (<%= UmbracoWorkflowInstance.Name %>)</h2>
        
        <p>
            Started: <%= UmbracoWorkflowInstance.StartTime %>
        </p>
        
        <p>Instantiator: <%= new User(UmbracoWorkflowInstance.Instantiator).Name %></p>
        
        <p>Instantiator Comment: <%= UmbracoWorkflowInstance.Comment %></p>
        
        <table>
        <thead>
            <th>
                Transition History
            </th>
        </thead>
            <tbody>
        <% foreach (var transition in UmbracoWorkflowInstance.TransitionHistory)
           { %>
        
        <tr>
            <td>
                <%= transition %>
            </td>
        </tr>


        <%   } %>
        </tbody>
        </table>
        

    </div>
</asp:Content>
