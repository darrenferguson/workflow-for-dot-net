using FergusonMoriyam.Workflow.Domain.Designer;
using FergusonMoriyam.Workflow.Infrastructure;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.Factory;
using FergusonMoriyam.Workflow.Interfaces.Domain.Designer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test.Database.MySql
{
    [TestClass]
    public class TestWorkflowDesignerPointRespositoryMySql
    {
        public TestWorkflowDesignerPointRespositoryMySql()
        {
            WorkflowDesignerPointRespository.Instance.Storage = TempFileStorage.Instance;
            WorkflowDesignerPointRespository.Instance.DatabaseHelper = DatabaseHelperFactory.Instance.CreateDatabaseHelper(
                   "FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.MySqlDatabaseHelper, FergusonMoriyam.Workflow.Infrastructure");

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
