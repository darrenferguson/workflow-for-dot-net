using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EndWorkflowTask = FergusonMoriyam.Workflow.Domain.Task.EndWorkflowTask;

namespace FergusonMoriyam.Workflow.Test
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
