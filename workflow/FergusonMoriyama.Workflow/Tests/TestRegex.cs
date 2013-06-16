using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test
{
    [TestClass]
    public class TestRegex
    {
        [TestMethod]
        public void TestMethod1()
        {

            var y = Regex.Replace("datalayer=SQLCE4Umbraco.SqlCEHelper,SQLCE4Umbraco;data source=|DataDirectory|\\Umbraco.sdf", "datalayer\\=.*?;", "");
            Console.WriteLine(y);
        }
    }
}
