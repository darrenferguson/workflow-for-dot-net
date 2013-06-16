using System.IO;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using Common.Logging;

namespace FergusonMoriyam.Workflow.Infrastructure
{
    public class TempFileStorage : IStorage
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly TempFileStorage Storage = new TempFileStorage();

        public static TempFileStorage Instance
        {
            get { return Storage; }
        }

        private TempFileStorage()
        {
            _tempPath = Path.GetTempPath();
        }
        #endregion

        private readonly string _tempPath;

        private string FileName(string identifier)
        {
            return Path.Combine(_tempPath, identifier);
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
