//using System;
//using System.Data.SqlServerCe;
//using System.IO;
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
//    [TestClass]
//    public class TestSqlCe
//    {
//        public TestSqlCe()
//        {

//            WorkflowConfigurationRepository.Instance.Storage = TempFileStorage.Instance;
//            WorkflowConfigurationRepository.Instance.DatabaseHelper =
//                DatabaseHelperFactory.Instance.CreateDatabaseHelper(
//                    "Moriyama.Workflow.Infrastructure.DatabaseHelper.SqlCeDatbaseHelper, Moriyama.Workflow.Infrastructure");

//            ((SqlCeDatbaseHelper)WorkflowConfigurationRepository.Instance.DatabaseHelper).ConnectionStringProvider
//                = new SqlCeConnectionStringProvider();

//            var file = "C:/Users/Darren/Source/wf/FergusonMoriyama.Workflow/Tests/data/test.sdf";
//            if(File.Exists(file))
//            {
//                File.Delete(file);
//            }

//            var conn = "Data Source="+file+";Persist Security Info=False;";
//            var engine = new SqlCeEngine(conn);
//            engine.CreateDatabase();

            

//            var cmd =
//                @"CREATE TABLE workflowconfiguration (
//	id int PRIMARY KEY IDENTITY,
//    name nvarchar(255) NOT NULL,
//    typename nvarchar(255) NOT NULL,
//    isconfigurationactive BIT NOT NULL DEFAULT 0,
//    locked BIT NOT NULL DEFAULT 0);
//
//CREATE TABLE point(
//	ownerId nvarchar(255) NOT NULL,
//    x int NOT NULL,
//    y int NOT NULL);
//
//CREATE TABLE workflowinstance(
//	id int PRIMARY KEY IDENTITY,
//    name nvarchar(255) NOT NULL,
//    typename nvarchar(255) NOT NULL,
//    instantiationtime datetime,
//    running BIT NOT NULL DEFAULT 0,
//    currenttask nvarchar(255));
//
//CREATE TABLE workflowcriteria(
//	id int PRIMARY KEY IDENTITY,
//    name nvarchar(255) NOT NULL);";

//           foreach(var s in cmd.Split(';'))
//           {
//               if (s.Trim().Length > 0)
//               {
//                   WorkflowConfigurationRepository.Instance.DatabaseHelper.CreateCommand(s).ExecuteNonQuery();

//               }
//           }

        

//        }

//        [TestMethod]
//        public void TestMethod1()
//        {
           
//            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "What the fuck!" };

//            WorkflowConfigurationRepository.Instance.Create(a);


//            foreach(var wf in WorkflowConfigurationRepository.Instance.List())
//            {
//                Console.Out.WriteLine(wf.Id);
//            }
//            WorkflowConfigurationRepository.Instance.DatabaseHelper.CloseConnection();
//        }

//        [TestMethod]
//        public void TestRetrieve()
//        {
//            var r = WorkflowConfigurationRepository.Instance;

//            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "What the fuck!" };

//            var task = new EndWorkflowTask { Name = "Test" };
//            a.StartTask = task;
//            //  a.Tasks.Add(task);

//            r.Create(a);
//            var id = a.Id;
//            a = null;

//            var b = r.GetById(id);
//            b = r.RestoreState(b);

//            Assert.IsNotNull(b);
//            WorkflowConfigurationRepository.Instance.DatabaseHelper.CloseConnection();
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
//            foreach (var item in y)
//            {
//                Console.WriteLine(item.Name);
//            }
//            Assert.IsNotNull(y);
//            WorkflowConfigurationRepository.Instance.DatabaseHelper.CloseConnection();
//        }

//        [TestMethod]
//        public void TestDelete()
//        {
//            var r = WorkflowConfigurationRepository.Instance;
//            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "Delete me" };

//            r.Create(a);

//            r.Delete(a);
//            WorkflowConfigurationRepository.Instance.DatabaseHelper.CloseConnection();
//        }


//        [TestMethod]
//        public void TestSave()
//        {
//            var r = WorkflowConfigurationRepository.Instance;
//            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "What the fuck!" };

//            r.Create(a);

//            a.Name = "the bomb";
//            r.Update(a);

//            a.IsConfigurationActive = true;
//            r.Update(a);
//            WorkflowConfigurationRepository.Instance.DatabaseHelper.CloseConnection();
//        }

//        [TestMethod]
//        public void TestSaveExtended()
//        {
//            var r = WorkflowConfigurationRepository.Instance;
//            var a = new WorkflowConfiguration { Name = "This is custom" };

//            r.Create(a);
//            WorkflowConfigurationRepository.Instance.DatabaseHelper.CloseConnection();
//        }

//    }

//    internal class SqlCeConnectionStringProvider : IConnectionStringProvider
//    {
//        public string GetConncetionString()
//        {
//            var file = "C:/Users/Darren/Source/wf/FergusonMoriyama.Workflow/Tests/data/test.sdf";
//            return "Data Source=" + file + ";Persist Security Info=False;";
//        }
//    }
//}
