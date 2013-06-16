using System.IO;

namespace FergusonMoriyam.Workflow.Interfaces.Infrastructure
{
    public interface IStorage
    {
        StreamReader GetReader(string identifier);
        StreamWriter GetWriter(string identifier);
        void Delete(string identifier);
    }
}
