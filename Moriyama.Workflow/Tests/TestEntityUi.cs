using System;
using System.Linq;
using Moriyama.Workflow.Domain;
using Moriyama.Workflow.Domain.Task;
using Moriyama.Workflow.Interfaces.Ui;
using Moriyama.Workflow.Ui;
using Moriyama.Workflow.Ui.Generic;
using Moriyama.Workflow.Ui.WorkflowConfigurationUi;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moriyama.Workflow.Tests
{
    [TestClass]
    public class TestEntityUi
    {
        [TestMethod]
        public void TestFindUi()
        {
            // apparently need to instantiate something for assembly to be loaded ??
            var ui = new NamePropertyUi();
            // var t = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var types = Application.Reflection.Helper.Instance.TypesImplementingInterface(typeof(IWorkflowEntityUi));

           
            Assert.IsTrue(types.Count() > 0);

        }

        [TestMethod]
        public void TestUiResolver()
        {
            // apparently need to instantiate something for assembly to be loaded ??
            var uix = new NamePropertyUi();

            // var t = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var ui = WorkflowEntityUiResolver.Instance.Resolve(new WorkflowConfiguration());



            Assert.IsNotNull(ui);
            Console.WriteLine(ui.GetType().ToString());
        }

        

        [TestMethod]
        public void TestNameProperty()
        {
           var ui = new NamePropertyUi();
           Console.WriteLine(ui.Label);

           var uiw = new IsConfigurationActivePropertyUi();
           Console.WriteLine(uiw.Label);
        }

         [TestMethod]
        public void TestUiFromEntityInstance()
        {
         
            // apparently need to instantiate something for assembly to be loaded ??
            var ui = new NamePropertyUi();
            var types = Application.Reflection.Helper.Instance.TypesImplementingInterface(typeof(IWorkflowEntityUi));

            var w = new WorkflowConfiguration();


            foreach(var x in types)
            {
                var ux = (IWorkflowEntityUi) Activator.CreateInstance(x);
                Console.WriteLine(ux.SupportsType(w));
            }

        }

        
        [TestMethod]
        public void TestResolveTaskUi()
        {

            var ui = WorkflowEntityUiResolver.Instance.Resolve(new EndWorkflowTask());
            Assert.IsNotNull(ui);

            ui = WorkflowEntityUiResolver.Instance.Resolve(new DelayWorkflowTask());
            Assert.IsNotNull(ui);

        }
    }
}
