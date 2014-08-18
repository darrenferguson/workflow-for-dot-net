using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Ui.Adapter;
using Moriyama.Workflow.Ui.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moriyama.Workflow.Tests
{
    [TestClass]
    public class TestWorkflowTaskJsonAdapter
    {
        [TestMethod]
        public void TestSerialize()
        {

            IWorkflowTask t = new DelayWorkflowTask();
            t.Name = "JSON test";
            
            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(WorkflowTaskUiAdapter));

            ser.WriteObject(stream1, WorkflowTaskUiAdapterFactory.Instance.CreateWorkflowTaskUiAdapter(t));

            stream1.Position = 0;
            var sr = new StreamReader(stream1);
            Console.Write("JSON form of Task object: ");
            Console.WriteLine(sr.ReadToEnd());



        }

        [TestMethod]
        public void TestSerializeCollection()
        {
            IWorkflowTask t = new DelayWorkflowTask { Name = "JSON test" };

            IWorkflowTask u = new EndWorkflowTask() {Name = "JSON test2"};
             
            var l = new List<IWorkflowTask> {t, u};

            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(WorkflowTaskCollectionUiAdapter), new List<Type> { typeof(WorkflowTaskUiAdapter) });
           
            ser.WriteObject(stream1, new WorkflowTaskCollectionUiAdapter(l));

            stream1.Position = 0;
            var sr = new StreamReader(stream1);
            Console.Write("JSON form of Task object: ");
            Console.WriteLine(sr.ReadToEnd());

            Console.WriteLine(new JavaScriptSerializer().Serialize(new WorkflowTaskCollectionUiAdapter(l)));



        }

       

    }
}
