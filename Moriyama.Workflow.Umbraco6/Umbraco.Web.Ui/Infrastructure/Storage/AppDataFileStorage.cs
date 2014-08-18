using System.IO;
using Moriyama.Workflow.Interfaces.Infrastructure;
using umbraco.IO;

namespace Moriyama.Workflow.Umbraco6.Web.Ui.Infrastructure.Storage
{
    public class AppDataFileStorage : IStorage
    {
        private static readonly AppDataFileStorage Storage = new AppDataFileStorage();

        public static AppDataFileStorage Instance
        {
            get { return Storage; }
        }

        private AppDataFileStorage()
        {
            _path = IOHelper.MapPath("~/App_Data/fmworkflow");
            if(!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        private readonly string _path;

        private string FileName(string identifier)
        {
            return Path.Combine(_path, identifier);
        }

        public StreamReader GetReader(string identifier)
        {
            return new StreamReader(FileName(identifier));
        }

        public StreamWriter GetWriter(string identifier)
        {
            return new StreamWriter(FileName(identifier));
        }

        public void Delete(string identifier)
        {
            File.Delete(FileName(identifier));
        }
    }
}