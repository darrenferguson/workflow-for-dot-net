using FergusonMoriyam.Workflow.Domain.Designer;
using FergusonMoriyam.Workflow.Infrastructure;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.Factory;
using FergusonMoriyam.Workflow.Interfaces.Domain.Designer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test.Database.SqlCe
{
    [TestClass]
    public class TestWorkflowDesignerPointRespositorySqlCe
    {
        public TestWorkflowDesignerPointRespositorySqlCe()
        {
            WorkflowConfigurationRepository.Instance.Storage = TempFileStorage.Instance;
            WorkflowConfigurationRepository.Instance.DatabaseHelper =
                DatabaseHelperFactory.Instance.CreateDatabaseHelper(
                    "FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.SqlCeDatbaseHelper, FergusonMoriyam.Workflow.Infrastructure");

            ((SqlCeDatbaseHelper)WorkflowConfigurationRepository.Instance.DatabaseHelper).ConnectionStringProvider
                = new SqlCeConnectionStringProvider();
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
