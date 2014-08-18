using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moriyama.Workflow.Domain;

namespace Moriyama.Workflow.Tests.Classes
{
    class ExtendedWorkflowConfiguration : WorkflowConfiguration
    {

        public string Custom { get; set; }
    }
}
