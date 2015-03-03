using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Domain.Designer;
using FergusonMoriyam.Workflow.Interfaces.Domain.Designer;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using log4net;


namespace FergusonMoriyam.Workflow.Infrastructure
{
    public class WorkflowDesignerPointRespository : Repository, IDesignerPointRepository<IPoint, string>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Instance
        private static readonly WorkflowDesignerPointRespository Repository = new WorkflowDesignerPointRespository();

        public static WorkflowDesignerPointRespository Instance
        {
            get { return Repository; }
        }

        private WorkflowDesignerPointRespository()
        {
        }
        #endregion

        public IList<IPoint> List(string ownerId)
        {
            var cmd = DatabaseHelper.CreateCommand("select * from point where OwnerId = @OwnerId");
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("OwnerId", ownerId));

            var configs = new List<IPoint>();
            var records = cmd.ExecuteReader();
            while (records.Read())
            {
                configs.Add(
                    new Point
                    {
                        OwnerId = (string)records["ownerId"],
                        X = (int)records["X"],
                        Y = (int)records["Y"]
                    }
                );
            }
            
            DatabaseHelper.CloseConnection();

            return configs;
        }

        public IList<IPoint> List(IEnumerable<string> ids)
        {
            var points = new List<IPoint>();
            foreach(var id in ids)
            {
                points.AddRange(List(id));
            }

            return points;
        }

        public void Create(IPoint saveObj)
        {

            var cmd = DatabaseHelper.CreateCommand("insert into point (ownerId, x, y) values (@OwnerId, @X, @Y);");

            cmd.Parameters.Add(DatabaseHelper.CreateParameter("OwnerId", saveObj.OwnerId));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("X", saveObj.X));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Y", saveObj.Y));

            cmd.ExecuteNonQuery();

            DatabaseHelper.CloseConnection();
        }

        public void Delete(string id)
        {
            var cmd = DatabaseHelper.CreateCommand("delete from point where ownerId = @OwnerId");
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("OwnerId", id));
            cmd.ExecuteScalar();
            DatabaseHelper.CloseConnection();
        }

        public void Delete(IEnumerable<string> ids)
        {
            foreach(var id in ids)
            {
                // Yeah I know.
                Delete(id);
            }
        }
    }
}
