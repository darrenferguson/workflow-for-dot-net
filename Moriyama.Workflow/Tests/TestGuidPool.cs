using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moriyama.Workflow.Application;
using Moriyama.Workflow.Interfaces.Application;

namespace Moriyama.Workflow.Tests
{
    [TestClass]
    public class TestGuidPool
    {
        [TestMethod]
        public void TestGenerateGuids()
        {
            IGuidPool g = new GuidPool();
            foreach(var s in g.CreateGuids(10))
            {
                Console.WriteLine(s);
            }
        }
    }
}
