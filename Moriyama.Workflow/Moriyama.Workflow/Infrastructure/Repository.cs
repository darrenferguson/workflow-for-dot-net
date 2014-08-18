using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Moriyama.Workflow.Interfaces.Infrastructure;
using Common.Logging;

namespace Moriyama.Workflow.Infrastructure
{
    public abstract class Repository
    {
        
        public IStorage Storage { get; set; }
        public IDatabaseHelper DatabaseHelper { get; set; }

        protected IFormatter Formatter = new BinaryFormatter();

    }
}
