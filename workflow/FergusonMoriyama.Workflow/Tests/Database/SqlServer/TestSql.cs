using System;
using System.Data;
using System.Data.SqlClient;
using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Domain.Task;
using FergusonMoriyam.Workflow.Infrastructure;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper;
using FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.Factory;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FergusonMoriyam.Workflow.Test.Database.SqlServer
{
    [TestClass]
    public class TestSql
    {
        private SqlConnection c;

        public TestSql()
        {
            WorkflowConfigurationRepository.Instance.Storage = TempFileStorage.Instance;
            WorkflowConfigurationRepository.Instance.DatabaseHelper =
                DatabaseHelperFactory.Instance.CreateDatabaseHelper(
                    "FergusonMoriyam.Workflow.Infrastructure.DatabaseHelper.SqlServerDatabaseHelper, FergusonMoriyam.Workflow.Infrastructure");

            ((SqlServerDatabaseHelper) WorkflowConfigurationRepository.Instance.DatabaseHelper).ConnectionStringProvider
                = new SqlServerConnectionStringProvider();

            
            c = new SqlConnection("server=.;database=workflow;user id=workflow;password=workflow");
            c.Open();

            new SqlCommand(@"
                IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='workflowconfiguration') DROP TABLE workflowconfiguration 
            ", c).ExecuteScalar();


            new SqlCommand(@"
              CREATE TABLE [dbo].[workflowconfiguration](
	[id] int PRIMARY KEY IDENTITY,
    [name] [varchar](255) NOT NULL,
    [typename] [varchar](255) NOT NULL,
    [isconfigurationactive] BIT NOT NULL DEFAULT 0,
    [locked] BIT NOT NULL DEFAULT 0)

             ", c).ExecuteScalar();


            new SqlCommand(@"
                IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='point') DROP TABLE point 
            ", c).ExecuteScalar();


            new SqlCommand(@"
              CREATE TABLE [dbo].[point](
	[ownerId] [varchar](255) NOT NULL,
    [x] [int] NOT NULL,
    [y] [int] NOT NULL)

             ", c).ExecuteScalar();


            new SqlCommand(@"
                IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='workflowinstance') DROP TABLE workflowinstance 
            ", c).ExecuteScalar();


            new SqlCommand(@"
              CREATE TABLE [dbo].[workflowinstance](
	[id] int PRIMARY KEY IDENTITY,
    [name] [varchar](255) NOT NULL,
    [typename] [varchar](255) NOT NULL,
    [instantiationtime] datetime,
    [running] BIT NOT NULL DEFAULT 0,
    [currenttask] [varchar](255))

             ", c).ExecuteScalar();



            new SqlCommand(@"
                IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='workflowcriteria') DROP TABLE workflowcriteria 
            ", c).ExecuteScalar();

            new SqlCommand(@"
              CREATE TABLE [dbo].[workflowcriteria](
	[id] int PRIMARY KEY IDENTITY,
    [name] [varchar](255) NOT NULL)

             ", c).ExecuteScalar();

            

        }

        [TestMethod]
        public void TestExecuteSql()
        {
            Assert.IsTrue(c.State == ConnectionState.Open);
            
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
}
