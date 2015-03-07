using System.IO;

namespace Moriyama.Workflow.Interfaces.Infrastructure
{
    public interface IStorage
    {
        StreamReader GetReader(string identifier);
        StreamWriter GetWriter(string identifier);
        void Delete(string identifier);
    }
}
