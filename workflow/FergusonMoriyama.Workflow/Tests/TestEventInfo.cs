using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FergusonMoriyam.Workflow.Domain.Event.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test
{
    [TestClass]
    public class TestEventInfo
    {
        [TestMethod]
        public void TestMethod1()
        {
            // var a = EventInformationCollectionFactory.Instance.Create();
            var a = EventInformationCollectionFactory.Instance.Create("Microsoft");
            Assert.IsTrue(a.Events.Count > 0);
        
        }
    }
}
