using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using umbraco.DataLayer;

namespace Umbraco.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string connectionString = "server=.;database=testbed4;user id=testbed4;password=testbed4";

            var c = new SqlConnection(connectionString);
            c.Open();
            c.Close();

            

        }
    }
}
