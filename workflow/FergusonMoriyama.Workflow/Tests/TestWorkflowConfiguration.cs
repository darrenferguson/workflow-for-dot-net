using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test
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
