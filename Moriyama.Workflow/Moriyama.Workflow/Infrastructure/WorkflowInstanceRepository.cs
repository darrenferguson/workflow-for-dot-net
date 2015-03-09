using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using Moriyama.Workflow.Domain;
using Moriyama.Workflow.Interfaces.Domain;
using Moriyama.Workflow.Interfaces.Infrastructure;
using log4net;

namespace Moriyama.Workflow.Infrastructure
{
    public class WorkflowInstanceRepository : Repository, IRepository<IWorkflowInstance, int>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Instance

        private static readonly WorkflowInstanceRepository Repository = new WorkflowInstanceRepository();

        public static WorkflowInstanceRepository Instance
        {
            get { return Repository; }
        }

        private WorkflowInstanceRepository()
        {
        }
        #endregion
       
        public IList<IWorkflowInstance> List()
        {
            var cmd = DatabaseHelper.CreateCommand("select * from workflowinstance");

            var configs = new List<IWorkflowInstance>();
            var records = cmd.ExecuteReader();

            while (records.Read())
            {
                configs.Add(
                    new WorkflowInstance
                    {
                        Id = (int)records["Id"],
                        Name = (string)records["Name"],
                        InstantiationTime = Convert.ToDateTime(records["InstantiationTime"]),
                        TypeName = (string)records["TypeName"],
                        Started = Convert.ToBoolean(records["Running"]),
                        Ended = Convert.ToBoolean(records["Ended"])
                    }
                );
            }

            DatabaseHelper.CloseConnection();
            return configs;
        }

        public IWorkflowInstance GetById(int id)
        {
            var cmd = DatabaseHelper.CreateCommand("select * from workflowinstance where id = @Id");
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", id));

            var records = cmd.ExecuteReader();
            records.Read();

            var i = new WorkflowInstance
            {
                Id = (int)records["Id"],
                Name = (string)records["Name"],
                InstantiationTime = Convert.ToDateTime(records["InstantiationTime"]),
                TypeName = (string)records["TypeName"],
                Started = Convert.ToBoolean(records["Running"]),
                Ended = Convert.ToBoolean(records["Ended"])
            };
            DatabaseHelper.CloseConnection();
            return i;
        }

        public IWorkflowInstance RestoreState(IWorkflowInstance obj)
        {
            using (var reader = Storage.GetReader(FileName(obj, obj.Id)))
            {
                return (IWorkflowInstance)Formatter.Deserialize(reader.BaseStream);
            }
        }

        public void Create(IWorkflowInstance saveObj)
        {

            var transaction = DatabaseHelper.BeginTransaction(); 
            
            try {
                var cmd = DatabaseHelper.CreateCommand("insert into workflowinstance (name, typename, instantiationtime, running, currenttask) values (@Name, @TypeName, @InstantiationTime, @Running, @CurrentTask);");

                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Name", saveObj.Name));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("TypeName", saveObj.GetType().FullName));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("InstantiationTime", saveObj.InstantiationTime));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Running", saveObj.Started));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Ended", saveObj.Ended));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("CurrentTask", saveObj.CurrentTask == null ? "" : saveObj.CurrentTask.Name));
                cmd.Transaction = transaction;

                cmd.ExecuteNonQuery();
                cmd.CommandText = DatabaseHelper.IdentityQuery;
                saveObj.Id = Convert.ToInt32(cmd.ExecuteScalar());

                transaction.Commit();
                Seralise(saveObj);
            }
            catch (DbException sqlError)
            {
                transaction.Rollback();
                Log.Warn(sqlError);
            }
            DatabaseHelper.CloseConnection();
        }

        public void Update(IWorkflowInstance saveObj)
        {
            var cmd = DatabaseHelper.CreateCommand("update workflowinstance set name = @Name, typename = @TypeName, instantiationtime = @InstantiationTime, running = @Running, currenttask = @CurrentTask, ended = @Ended where id = @Id");

            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Name", saveObj.Name));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("TypeName", saveObj.GetType().FullName));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("InstantiationTime", saveObj.InstantiationTime));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Running", saveObj.Started));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Ended", saveObj.Ended));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("CurrentTask", saveObj.CurrentTask == null ? "" : saveObj.CurrentTask.Name));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", saveObj.Id));

            cmd.ExecuteNonQuery();
            DatabaseHelper.CloseConnection();

            Seralise(saveObj);
        }

        public void Delete(IWorkflowInstance delObj)
        {
            var cmd = DatabaseHelper.CreateCommand("delete from workflowinstance where id = @Id");

            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", delObj.Id));

            cmd.ExecuteNonQuery();
            DatabaseHelper.CloseConnection();

            Storage.Delete(FileName(delObj, delObj.Id));
        }

        protected void Seralise(IWorkflowInstance saveObj)
        {
            using (var writer = Storage.GetWriter(FileName(saveObj, saveObj.Id)))
            {
                Formatter.Serialize(writer.BaseStream, saveObj);
                writer.Close();
            }
        }

        protected string FileName(IWorkflowInstance o, object id)
        {
            return "WorkflowInstanceRepository_" + id + ".bin";
        }
    }
}
