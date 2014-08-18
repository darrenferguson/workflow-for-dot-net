//using System;
//using Moriyama.Workflow.Domain;
//using Moriyama.Workflow.Domain.Task;
//using Moriyama.Workflow.Infrastructure;
//using Moriyama.Workflow.Infrastructure.DatabaseHelper;
//using Moriyama.Workflow.Infrastructure.DatabaseHelper.Factory;
//using Moriyama.Workflow.Interfaces.Domain;
//using Moriyama.Workflow.Interfaces.Infrastructure;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Moriyama.Workflow.Tests.Database.SqlCe
//{
//    internal class SqlServerConnectionStringProvider : IConnectionStringProvider
//    {
//        public string GetConncetionString()
//        {
//            return "server=DARREN-LAPTOP\\SQLEXPRESS;database=workflow;user id=workflow;password=workflow";
//        }
//    }

//    [TestClass]
//    public class TestWorkflowConfigRepositorySqlServer
//    {
//        public TestWorkflowConfigRepositorySqlServer()
//        {
//            WorkflowConfigurationRepository.Instance.Storage = TempFileStorage.Instance;
//            WorkflowConfigurationRepository.Instance.DatabaseHelper =
//                DatabaseHelperFactory.Instance.CreateDatabaseHelper(
//                    "Moriyama.Workflow.Infrastructure.DatabaseHelper.SqlCeDatbaseHelper, Moriyama.Workflow.Infrastructure");

//            ((SqlCeDatbaseHelper)WorkflowConfigurationRepository.Instance.DatabaseHelper).ConnectionStringProvider
//                = new SqlCeConnectionStringProvider();

//        }

//        [TestMethod]
//        public void TestRetrieve()
//        {
//            var r = WorkflowConfigurationRepository.Instance;

//            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "What the fuck!" };

//            var task = new EndWorkflowTask {Name = "Test"};
//            a.StartTask = task;
//           //  a.Tasks.Add(task);

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
//            var r = WorkflowConfigurationRepository.Instance;
//            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "Get Me" };
            
//            var b = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "Get Me Too" };
//            // b.StartTask = new EndWorkflowTask();
            
//            //r.Save(a);
//            //r.Save(b);

//            var id = a.Id;
//            a = null;

//            var y = r.List();
//            foreach(var item in y)
//            {
//                Console.WriteLine(item.Name);
//            }
//            Assert.IsNotNull(y);
//        }

//        [TestMethod]
//        public void TestDelete()
//        {
//            var r = WorkflowConfigurationRepository.Instance;
//            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "Delete me" };

//            r.Create(a);

//            r.Delete(a);
//        }


//        [TestMethod]
//        public void TestSave()
//        {
//            var r = WorkflowConfigurationRepository.Instance;
//            var a = (IWorkflowConfiguration) new WorkflowConfiguration {Name = "What the fuck!"};

//            r.Create(a);
            
//            a.Name = "the bomb";
//            r.Update(a);

//            a.IsConfigurationActive = true;
//            r.Update(a); 
//        }

//        [TestMethod]
//        public void TestSaveExtended()
//        {
//            var r = WorkflowConfigurationRepository.Instance;
//            var a = new WorkflowConfiguration { Name = "This is custom" };

//            r.Create(a);  
//        }

//    }
//}
