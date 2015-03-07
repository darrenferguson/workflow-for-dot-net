using System;
using System.Collections.Generic;
using System.Net.Mail;
using Moriyama.Workflow.Interfaces.Application.Runtime;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Umbraco6.Application;
using Moriyama.Workflow.Umbraco6.Domain;
using umbraco.BusinessLogic;
using umbraco.IO;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.media;

namespace Moriyama.Workflow.Umbraco6.Task
{
    [Serializable]
    public class TemplateEmailWithMediaWorkflowTask : TemplateEmailWorkflowTask
    {

        public override void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            base.Run(workflowInstance, runtime);

            var body = Helper.Instance.RenderTemplate(RenderTemplate);
            
            IList<string> files = new List<string>();

            foreach(var nodeId in ((UmbracoWorkflowInstance) workflowInstance).CmsNodes)
            {
                var node = new CMSNode(nodeId);
                if(node.IsMedia())
                {
                    files.Add(IOHelper.MapPath((string) new Media(nodeId).getProperty("umbracoFile").Value));
                }
            }

            var f = new User(From).Email;
            foreach(var r in GetRecipients())
            {
                var mail = new MailMessage(f, r) {Subject = Subject, IsBodyHtml = true, Body = body};

                foreach(var file in files)
                {
                    var attach = new Attachment(file);
                    mail.Attachments.Add(attach);
                }

                var smtpClient = new SmtpClient();
                smtpClient.Send(mail);
            }

            runtime.Transition(workflowInstance, this, "done");
        }
    }
    
}
