using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Ui;

namespace Moriyama.Workflow.Ui
{
    public class WorkflowEntityUiResolver : IUiResolver
    {
        public IGlobalisationService TheGlobalisationService { get; set; }
        
        private readonly IEnumerable<Type> _uis;

        private static readonly WorkflowEntityUiResolver Resolver = new WorkflowEntityUiResolver();

        public static WorkflowEntityUiResolver Instance
        {
            get { return Resolver; }
        }

        private WorkflowEntityUiResolver()
        {
            _uis = Application.Reflection.Helper.Instance.TypesImplementingInterface(typeof(IWorkflowEntityUi)); 
        }

        public IWorkflowEntityUi Resolve(object typeToFindUiFor)
        {
            foreach(var uiType in _uis)
            {
                var o = FormatterServices.GetUninitializedObject(uiType);
                if (typeof(IGlobalisable).IsAssignableFrom(o.GetType()))
                {
                    ((IGlobalisable)o).TheGlobalisationService = TheGlobalisationService;
                }

                var ctor = uiType.GetConstructors()[0];
                ctor.Invoke(o, new object[]{});

                var theUi = (IWorkflowEntityUi)o;

                if (theUi.SupportsType(typeToFindUiFor))
                {
                    foreach (var t in theUi.UiProperties)
                    {
                        if (typeof(IGlobalisable).IsAssignableFrom(t.GetType()))
                        {
                            ((IGlobalisable)t).TheGlobalisationService = TheGlobalisationService;
                        }
                    }

                    return theUi;
                }
            }
            return null;

            // return _uis.Select(ui => (IWorkflowEntityUi) Activator.CreateInstance(ui)).Where(i => i.SupportsType(typeToFindUiFor)).FirstOrDefault();
        }
    }
}