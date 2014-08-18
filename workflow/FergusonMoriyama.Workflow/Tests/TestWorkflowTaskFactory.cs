using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Moriyama.Workflow.Domain.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moriyama.Workflow.Tests
{
    [TestClass]
    public class TestWorkflowTaskFactory
    {
        [TestMethod]
        public void TestConstruction()
        {
            var task = WorkflowTaskFactory.Instance.CreateTask("Moriyama.Workflow.Domain.Task.DummyWorkflowTask");

        }
    }
}
