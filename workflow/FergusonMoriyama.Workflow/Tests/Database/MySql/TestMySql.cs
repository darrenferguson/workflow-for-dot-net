using System;
using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Infrastructure;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.Factory;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test.Database.MySql
{
    [TestClass]
    public class TestMySql
    {
        public TestMySql()
        {

            WorkflowConfigurationRepository.Instance.Storage = TempFileStorage.Instance;
            WorkflowConfigurationRepository.Instance.DatabaseHelper = DatabaseHelperFactory.Instance.CreateDatabaseHelper(
                   "FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.MySqlDatabaseHelper, FergusonMoriyam.Workflow.Infrastructure");

            //((MySqlDatabaseHelper)WorkflowConfigurationRepository.Instance.DatabaseHelper).ConnectionStringProvider
            //    = new MysqlConnectionStringProvider();

            var sql =
                @"drop table if exists workflowconfiguration; 

CREATE TABLE workflowconfiguration (
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name nvarchar(255) NOT NULL,
    typename nvarchar(255) NOT NULL,
    isconfigurationactive BIT NOT NULL DEFAULT 0,
    locked BIT NOT NULL DEFAULT 0);

drop table if exists point; CREATE TABLE point(
	ownerId nvarchar(255) NOT NULL,
    x int NOT NULL,
    y int NOT NULL);

drop table if exists workflowinstance; CREATE TABLE workflowinstance(
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name nvarchar(255) NOT NULL,
    typename nvarchar(255) NOT NULL,
    instantiationtime datetime,
    running BIT NOT NULL DEFAULT 0,
    currenttask nvarchar(255));

drop table if exists workflowcriteria; CREATE TABLE workflowcriteria(
	id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
    name nvarchar(255) NOT NULL); commit;

set AUTOCOMMIT = 1;";


            WorkflowConfigurationRepository.Instance.DatabaseHelper.CreateCommand(sql).ExecuteNonQuery();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "What the fuck!" };
            WorkflowConfigurationRepository.Instance.Create(a);

            foreach (var wf in WorkflowConfigurationRepository.Instance.List())
            {
                Console.Out.WriteLine(wf.Id);
            }
        }

        [TestMethod]
        public void TestRetrieve()
        {
            var r = WorkflowConfigurationRepository.Instance;

            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "What the fuck!" };

            var task = new EndWorkflowTask { Name = "Test" };
            a.StartTask = task;
            //  a.Tasks.Add(task);

            r.Create(a);
            var id = a.Id;
            a = null;

            var b = r.GetById(id);
            b = r.RestoreState(b);

            Assert.IsNotNull(b);
        }

        [TestMethod]
        public void TestList()
        {
            var r = WorkflowConfigurationRepository.Instance;
            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "Get Me" };

            var b = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "Get Me Too" };
            // b.StartTask = new EndWorkflowTask();

            //r.Save(a);
            //r.Save(b);

            var id = a.Id;
            a = null;

            var y = r.List();
            foreach (var item in y)
            {
                Console.WriteLine(item.Name);
            }
            Assert.IsNotNull(y);
        }

        [TestMethod]
        public void TestDelete()
        {
            var r = WorkflowConfigurationRepository.Instance;
            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "Delete me" };

            r.Create(a);

            r.Delete(a);
        }


        [TestMethod]
        public void TestSave()
        {
            var r = WorkflowConfigurationRepository.Instance;
            var a = (IWorkflowConfiguration)new WorkflowConfiguration { Name = "What the fuck!" };

            r.Create(a);

            a.Name = "the bomb";
            r.Update(a);

            a.IsConfigurationActive = true;
            r.Update(a);
        }

        [TestMethod]
        public void TestSaveExtended()
        {
            var r = WorkflowConfigurationRepository.Instance;
            var a = new WorkflowConfiguration { Name = "This is custom" };

            r.Create(a);
        }
    }

    internal class MysqlConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConncetionString()
        {
            return "SERVER=localhost;DATABASE=u2;UID=root";
        }
    }

}
