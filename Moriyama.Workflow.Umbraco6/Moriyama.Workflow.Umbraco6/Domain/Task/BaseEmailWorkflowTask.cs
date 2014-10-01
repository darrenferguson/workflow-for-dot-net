using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Moriyama.Workflow.Application.Globalisation;
using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Domain;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic;

namespace Moriyama.Workflow.Umbraco6.Domain.Task
{
    [Serializable]
    public abstract class BaseEmailWorkflowTask : BaseWorkflowTask, IWorkflowTask
    {

        public bool MailInstantiator { get; set; }
        public bool MailNodeOwners { get; set; }

        public IList<int> UserTypes { get; set; }
        public IList<int> Users { get; set; }

        public int From { get; set; }
        public string Subject { get; set; }

        protected int Instantiator { get; set; }
        protected IList<int> CmsNodes { get; set; }

        protected string Comment { get; set; }
        protected IList<string> TransitionHistory { get; set; }
        
        public virtual void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            Instantiator = ((UmbracoWorkflowInstance)workflowInstance).Instantiator;
            CmsNodes = ((UmbracoWorkflowInstance)workflowInstance).CmsNodes;

            Comment = workflowInstance.Comment;
            TransitionHistory = workflowInstance.TransitionHistory;
        }

        protected string ReplaceTokens(string text)
        {
            text = text.Replace(Environment.NewLine, "<br/>");
            text = text.Replace("{Comment}", Comment);
            text = text.Replace("{Instantiator}", User.GetUser(Instantiator).Name);
            text = text.Replace("{Transitions}", string.Join(Environment.NewLine + "<br/>", TransitionHistory));

            return text;
        }

        protected BaseEmailWorkflowTask() : base()
        {
            AvailableTransitions.Add("done");
        }

        protected void SendMail(string body)
        {
            var from = new User(From).Email;
            
            foreach (var person in GetRecipients()) 
                SendEmail(from, person, Subject, body, true);
            
        }

        protected virtual void SendEmail(string from, string person, string subject, string body, bool html)
        {
            subject = ReplaceTokens(subject);
            body = ReplaceTokens(body);

            umbraco.library.SendMail(from, person, subject, body, html);
        }

        public virtual string GetAttachmentLinks(IEnumerable<int> attachedNodes)
        {
            var s = new StringBuilder();
            s.Append("<br/><br/>");

            var host = HttpContext.Current.Request.Url.Host;
            if (HttpContext.Current.Request.Url.Port != 80) host += ":" + HttpContext.Current.Request.Url.Port;

            foreach(var nodeId in attachedNodes)
            {
                var node = new CMSNode(nodeId);

                if(node.IsDocument())
                {
                    s.Append(string.Format("{2} ({1}): <a href='http://{0}/umbraco/dialogs/preview.aspx?id={1}'>[" + GlobalisationService.Instance.GetString("preview") + "]</a> ", host, nodeId, node.Text) + Environment.NewLine);
                    s.Append(string.Format("<a href='http://{0}/umbraco/actions/editContent.aspx?id={1}'>[" + GlobalisationService.Instance.GetString("edit") + "]</a><br/>", host, nodeId) + Environment.NewLine);

                } else if(node.IsMedia())
                {
                    s.Append(string.Format("{2} ({1}): <a href='http://{0}/umbraco/dialogs/preview.aspx?id={1}'>{2}</a> ", host, nodeId, node.Text) + Environment.NewLine);
                    
                } else
                {
                    s.Append(node.Text + "<br/>" + Environment.NewLine);
                }
            }
            
            return s.ToString();
        }

        protected string GetUserEmail(int id)
        {
            return User.GetUser(id).Email;
        }

        protected IEnumerable<string> GetRecipients()
        {
            var recipients = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if(MailInstantiator) recipients.Add(GetUserEmail(Instantiator));

            if(MailNodeOwners)
            {
                foreach(var nodeId in CmsNodes)
                {
                    var e = new CMSNode(nodeId).User.Email;
                    recipients.Add(e);
                }
            }

            if (Users != null)
            {
                foreach (var user in Users)
                {
                    recipients.Add(GetUserEmail(user));
                }
            }

            if (UserTypes != null)
            {
                foreach (var userTypeId in UserTypes)
                {
                    foreach (var user in User.getAll().Where(u => u.UserType.Id == userTypeId))
                    {
                        recipients.Add(user.Email);
                    }
                }
            }
            return recipients;
        }

       
    }
}
