using System;
using FergusonMoriyam.Workflow.Application;
using FergusonMoriyam.Workflow.Interfaces.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test
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
