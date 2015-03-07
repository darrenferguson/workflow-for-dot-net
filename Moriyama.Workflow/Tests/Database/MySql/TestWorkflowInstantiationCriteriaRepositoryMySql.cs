//using System;
//using Moriyama.Workflow.Domain;
//using Moriyama.Workflow.Infrastructure;
//using Moriyama.Workflow.Infrastructure.DatabaseHelper;
//using Moriyama.Workflow.Infrastructure.DatabaseHelper.Factory;
//using Moriyama.Workflow.Interfaces.Domain;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Moriyama.Workflow.Tests.Database.MySql
//{
//    [TestClass]
//    public class TestWorkflowInstantiationCriteriaRepositoryMySql
//    {
//        public TestWorkflowInstantiationCriteriaRepositoryMySql()
//        {
//            WorkflowInstantiationCriteriaRepository.Instance.Storage = TempFileStorage.Instance;
//            WorkflowInstantiationCriteriaRepository.Instance.DatabaseHelper = DatabaseHelperFactory.Instance.CreateDatabaseHelper(
//                   "Moriyama.Workflow.Infrastructure.DatabaseHelper.MySqlDatabaseHelper, Moriyama.Workflow.Infrastructure");

//            ((MySqlDatabaseHelper)WorkflowInstantiationCriteriaRepository.Instance.DatabaseHelper).ConnectionStringProvider
//                = new MysqlConnectionStringProvider();
//        }

//        [TestMethod]
//        public void TestCreate()
//        {
//            var r = WorkflowInstantiationCriteriaRepository.Instance;

//            var a = (IWorkflowInstantiationCriteria)new WorkflowInstantiationCriteria { Name = "What the fuck!" };
            
//            r.Create(a);
//        }

//        [TestMethod]
//        public void TestRetrieve()
//        {
//            var r = WorkflowInstantiationCriteriaRepository.Instance;

//            var a = (IWorkflowInstantiationCriteria)new WorkflowInstantiationCriteria { Name = "What the fuck!" };

//            r.Create(a);
//            var id = a.Id;
//            a = null;

//            var b = r.GetById(id);
//            b = r.RestoreState(b);

//            Assert.IsNotNull(b);
//        }

//        [TestMethod]
//        public void TestList()
//        {
//            var r = WorkflowInstantiationCriteriaRepository.Instance;
//            var a = (IWorkflowInstantiationCriteria)new WorkflowInstantiationCriteria { Name = "Get Me" };

//            var b = (IWorkflowInstantiationCriteria)new WorkflowInstantiationCriteria { Name = "Get Me Too" };
//            // b.StartTask = new EndWorkflowTask();

//            //r.Save(a);
//            //r.Save(b);

//            var id = a.Id;
//            a = null;

//            var y = r.List();
//            foreach (var item in y)
//            {
//                Console.WriteLine(item.Name);
//            }
//            Assert.IsNotNull(y);
//        }

//        [TestMethod]
//        public void TestDelete()
//        {
//            var r = WorkflowInstantiationCriteriaRepository.Instance;
//            var a = (IWorkflowInstantiationCriteria)new WorkflowInstantiationCriteria { Name = "Delete me" };

//            r.Create(a);

//            r.Delete(a);
//        }


//        [TestMethod]
//        public void TestSave()
//        {
//            var r = WorkflowInstantiationCriteriaRepository.Instance;
//            var a = (IWorkflowInstantiationCriteria)new WorkflowInstantiationCriteria { Name = "What the fuck!" };

//            r.Create(a);

//            a.Name = "the bomb";
//            r.Update(a);

            
//            r.Update(a);
//        }

//    }
//}
