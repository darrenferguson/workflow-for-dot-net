using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test
{
    [TestClass]
    public class Reflection
    {
        [TestMethod]
        public void TestMethod1()
        {
            var t = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var u = t.SelectMany(s => s.GetTypes());

        }
    }
}
