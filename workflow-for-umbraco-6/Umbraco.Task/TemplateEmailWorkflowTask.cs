using System;
using FergusonMoriyam.Workflow.Interfaces.Application.Runtime;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Umbraco.Application;
using FergusonMoriyam.Workflow.Umbraco.Domain;
using FergusonMoriyam.Workflow.Umbraco.Domain.Task;

namespace FergusonMoriyam.Workflow.Umbraco.Task
{
    [Serializable]
    public class TemplateEmailWorkflowTask : BaseEmailWorkflowTask, IWorkflowTask, IRunnableWorkflowTask
    {
        public int RenderTemplate { get; set; }

        public override void Run(IWorkflowInstance workflowInstance, IWorkflowRuntime runtime)
        {
            base.Run(workflowInstance, runtime);

            var nodes = ((UmbracoWorkflowInstance) workflowInstance).CmsNodes;

            var body = Helper.Instance.RenderTemplate(RenderTemplate) + GetAttachmentLinks(nodes);

            SendMail(body);
            runtime.Transition(workflowInstance, this, "done");
        }
    }
}
