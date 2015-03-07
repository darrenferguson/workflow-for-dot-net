using Moriyama.Workflow.Domain.Designer;
using Moriyama.Workflow.Infrastructure;
using Moriyama.Workflow.Infrastructure.DatabaseHelper;
using Moriyama.Workflow.Infrastructure.DatabaseHelper.Factory;
using Moriyama.Workflow.Interfaces.Domain.Designer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moriyama.Workflow.Tests.Database.MySql
{
    [TestClass]
    public class TestWorkflowDesignerPointRespositoryMySql
    {
        public TestWorkflowDesignerPointRespositoryMySql()
        {
            WorkflowDesignerPointRespository.Instance.Storage = TempFileStorage.Instance;
            WorkflowDesignerPointRespository.Instance.DatabaseHelper = DatabaseHelperFactory.Instance.CreateDatabaseHelper(
                   "Moriyama.Workflow.Infrastructure.DatabaseHelper.MySqlDatabaseHelper, Moriyama.Workflow.Infrastructure");

            //((MySqlDatabaseHelper)WorkflowDesignerPointRespository.Instance.DatabaseHelper).ConnectionStringProvider
            //    = new MysqlConnectionStringProvider();
        }

        [TestMethod]
        public void TestSave()
        {
            var r = WorkflowDesignerPointRespository.Instance;
            var a = (IPoint)new Point { OwnerId = "Darren", X = 10, Y = 10 };

            r.Delete(a.OwnerId);
            r.Create(a);

            a = (IPoint)new Point { OwnerId = "Ferguson !", X = 67, Y = 89 };
            r.Delete(a.OwnerId);
            r.Create(a);
        }


        [TestMethod]
        public void TestList()
        {
            var r = WorkflowDesignerPointRespository.Instance;

            var items = r.List("Darren");
            Assert.IsTrue(items.Count > 0);
        }
    }
}
