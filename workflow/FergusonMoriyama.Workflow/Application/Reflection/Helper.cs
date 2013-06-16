using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FergusonMoriyam.Workflow.Domain;
using System.Web.Script.Serialization;
using FergusonMoriyam.Workflow.Interfaces.Application;
using Common.Logging;

namespace FergusonMoriyam.Workflow.Application.Reflection
{
    public class Helper : IGlobalisable
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private Helper()
        {
            JsSerializer = new JavaScriptSerializer();
        }

        private static readonly Helper Help = new Helper();

        public static Helper Instance
        {
            get { return Help; }
        }
        #endregion

        #region properties
        public IGlobalisationService TheGlobalisationService
        {
            get;
            set;
        }
        public JavaScriptSerializer JsSerializer { get; set; }
        #endregion

        public object SetProperties(object o, IDictionary<string, object> properties)
        {
            foreach (var propertyName in properties.Keys)
            {
                var objProperty = o.GetType().GetProperty(propertyName);
                
                if (objProperty == null) continue;
                var propertyValue = properties[propertyName];
                if (propertyValue == null) continue;

                var destPropertyType = objProperty.PropertyType;

                if (
                    destPropertyType != typeof(string) && 
                    destPropertyType != typeof(int) && 
                    destPropertyType != typeof(bool) && 
                    destPropertyType != typeof(Guid) && 
                    destPropertyType != typeof(IList<string>) && 
                    destPropertyType != typeof(IList<int>)
                    
                    ) continue;

                var sourcePropertyType = properties[propertyName].GetType();

                if(sourcePropertyType.IsArray)
                {
                    if( ((object[]) propertyValue).Length > 0)
                    {
                        var destType = ((object[]) propertyValue)[0].GetType();
                        if (destType == typeof (int))
                        {
                            propertyValue = ToList<int>((object[]) propertyValue);
                        } 
                        else
                        {
                            propertyValue = ToList<string>((object[])propertyValue);
                        }
                        sourcePropertyType = propertyValue.GetType();
                    } else
                    {
                        continue;
                    }
                }

                if (propertyValue == null || propertyValue.ToString() == "null") continue;

                try
                {
                    objProperty.SetValue(o,
                                         (destPropertyType == sourcePropertyType ||
                                          destPropertyType.IsAssignableFrom(sourcePropertyType))
                                             ? propertyValue
                                             : Convert.ChangeType(propertyValue, objProperty.PropertyType), null);

                } catch {}
            }

            return o;
        }


        public string Epoch()
        {
            var epoch = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return Math.Round(epoch.TotalMilliseconds, 0).ToString();
        }

        protected List<T> ToList<T>(object[] array)
        {
            return array.Select(item => (T) item).ToList();
        }

        protected IEnumerable<Assembly> NonDynamicAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !assembly.IsDynamic);
        }

        public IList<Type> WorkflowConfigurationTypes()
        {
            return NonDynamicAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(
                    type => type.IsSubclassOf(typeof (WorkflowConfiguration))).ToList();
        }

        public IEnumerable<Type> TypesImplementingInterface(Type t)
        {
            return NonDynamicAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => t.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract && p.Name != "UiWorkflowTask");
        }

        public object GetPropertyValue(object o, string name)
        {
            return o.GetType().GetProperty(name).GetValue(o, null);
        }

        public bool HasProperty(object o, string name)
        {
            return o.GetType().GetProperty(name) != null;
        }

        public T GetProperty<T>(object o, string name)
        {
            return (T) GetPropertyValue(o, name);
        }
    }
}
