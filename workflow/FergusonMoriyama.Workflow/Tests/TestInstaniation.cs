using Moriyama.Workflow.Domain;
using Moriyama.Workflow.Interfaces.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EndWorkflowTask = Moriyama.Workflow.Domain.Task.EndWorkflowTask;

namespace Moriyama.Workflow.Tests
{
    [TestClass]
    public class TestInstaniation
    {
        [TestMethod]
        public void BasicInstantiation()
        {
            IWorkflowConfiguration c = new WorkflowConfiguration {Name = "Test", IsConfigurationActive = true};

            IWorkflowTask t = new EndWorkflowTask();
            c.StartTask = t;
            c.Tasks.Add(t);

            var i = c.CreateInstance();

        }
    }
}
