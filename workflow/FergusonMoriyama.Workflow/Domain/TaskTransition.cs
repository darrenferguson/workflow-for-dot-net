using System;
using FergusonMoriyam.Workflow.Interfaces.Domain;

namespace FergusonMoriyam.Workflow.Domain
{
    class TaskTransition : ITaskTransition
    {
        public string TransitonName { get; set; }
        public Guid To { get; set; }
    }
}
