using System;
using Moriyama.Workflow.Interfaces.Domain;

namespace Moriyama.Workflow.Domain
{
    class TaskTransition : ITaskTransition
    {
        public string TransitonName { get; set; }
        public Guid To { get; set; }
    }
}
