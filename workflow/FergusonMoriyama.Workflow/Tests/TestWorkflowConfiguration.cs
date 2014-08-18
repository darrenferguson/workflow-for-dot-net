using Moriyama.Workflow.Domain;
using Moriyama.Workflow.Interfaces.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moriyama.Workflow.Tests
{
    [TestClass]
    public class TestWorkflowConfiguration
    {
        [TestMethod]
        public void TestInstantiate()
        {
            IWorkflowConfiguration workflowConfiguration = new WorkflowConfiguration { Name = "Testing 123" };

            Assert.IsFalse(workflowConfiguration.IsConfigurationActive);
            Assert.IsNotNull(workflowConfiguration.Name);

            Assert.IsNotNull(((IWorkflowInstantiator) workflowConfiguration).CreateInstance());

        }
    }
}
