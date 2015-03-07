//using Moriyama.Workflow.Domain.Designer;
//using Moriyama.Workflow.Infrastructure;
//using Moriyama.Workflow.Infrastructure.DatabaseHelper;
//using Moriyama.Workflow.Infrastructure.DatabaseHelper.Factory;
//using Moriyama.Workflow.Interfaces.Domain.Designer;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Moriyama.Workflow.Tests.Database.SqlCe
//{
//    [TestClass]
//    public class TestWorkflowDesignerPointRespositorySqlCe
//    {
//        public TestWorkflowDesignerPointRespositorySqlCe()
//        {
//            WorkflowConfigurationRepository.Instance.Storage = TempFileStorage.Instance;
//            WorkflowConfigurationRepository.Instance.DatabaseHelper =
//                DatabaseHelperFactory.Instance.CreateDatabaseHelper(
//                    "Moriyama.Workflow.Infrastructure.DatabaseHelper.SqlCeDatbaseHelper, Moriyama.Workflow.Infrastructure");

//            ((SqlCeDatbaseHelper)WorkflowConfigurationRepository.Instance.DatabaseHelper).ConnectionStringProvider
//                = new SqlCeConnectionStringProvider();
//        }

//        [TestMethod]
//        public void TestSave()
//        {
//            var r = WorkflowDesignerPointRespository.Instance;
//            var a = (IPoint)new Point { OwnerId = "Darren", X = 10, Y = 10 };

//            r.Delete(a.OwnerId);
//            r.Create(a);

//            a = (IPoint)new Point { OwnerId = "Ferguson !", X = 67, Y = 89 };
//            r.Delete(a.OwnerId);
//            r.Create(a);
//        }


//        [TestMethod]
//        public void TestList()
//        {
//            var r = WorkflowDesignerPointRespository.Instance;

//            var items = r.List("Darren");
//            Assert.IsTrue(items.Count > 0);
//        }
//    }
//}
