using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FergusonMoriyam.Workflow.Domain.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test
{
    [TestClass]
    public class TestWorkflowTaskFactory
    {
        [TestMethod]
        public void TestConstruction()
        {
            var task = WorkflowTaskFactory.Instance.CreateTask("FergusonMoriyam.Workflow.Domain.Task.DummyWorkflowTask");

        }
    }
}
