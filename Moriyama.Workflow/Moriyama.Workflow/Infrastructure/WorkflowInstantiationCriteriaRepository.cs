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
    public class WorkflowInstantiationCriteriaRepository : Repository, IRepository<IWorkflowInstantiationCriteria, int>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region instance
        private static readonly WorkflowInstantiationCriteriaRepository Repository = new WorkflowInstantiationCriteriaRepository();

        public static WorkflowInstantiationCriteriaRepository Instance
        {
            get { return Repository; }
        }

        private WorkflowInstantiationCriteriaRepository()
        {
        }
        #endregion

        
        public IList<IWorkflowInstantiationCriteria> List()
        {
            var cmd = DatabaseHelper.CreateCommand("select * from workflowcriteria");

            var configs = new List<IWorkflowInstantiationCriteria>();
            var records = cmd.ExecuteReader();
            while (records.Read())
            {
                configs.Add(
                    new WorkflowInstantiationCriteria
                    {
                        Id = (int)records["Id"],
                        Name = (string)records["Name"]
                    }
                );
            }
            DatabaseHelper.CloseConnection();
            return configs;
        }

        public IWorkflowInstantiationCriteria GetById(int id)
        {
            var cmd = DatabaseHelper.CreateCommand("select * from workflowcriteria where id = @Id");
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", id));

            var records = cmd.ExecuteReader();
            records.Read();

            var c=  new WorkflowInstantiationCriteria
            {
                Id = (int)records["Id"],
                Name = (string)records["Name"]
               
            };
            DatabaseHelper.CloseConnection();
            return c;

        }

        public IWorkflowInstantiationCriteria RestoreState(IWorkflowInstantiationCriteria obj)
        {
            using (var reader = Storage.GetReader(FileName(obj, obj.Id)))
            {
                return (IWorkflowInstantiationCriteria)Formatter.Deserialize(reader.BaseStream);
            }
        }

        public void Create(IWorkflowInstantiationCriteria saveObj)
        {
            var transaction = DatabaseHelper.BeginTransaction();

            try
            {
                var cmd = DatabaseHelper.CreateCommand("insert into workflowcriteria (name) values (@Name);");

                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Name", saveObj.Name));
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

        public void Update(IWorkflowInstantiationCriteria saveObj)
        {
            var cmd = DatabaseHelper.CreateCommand("update workflowcriteria set name = @Name where id = @Id");

            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Name", saveObj.Name));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", saveObj.Id));

            cmd.ExecuteNonQuery();
            DatabaseHelper.CloseConnection();

            Seralise(saveObj);
        }

        public void Delete(IWorkflowInstantiationCriteria delObj)
        {
            var cmd = DatabaseHelper.CreateCommand("delete from workflowcriteria where id = @Id");

            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", delObj.Id));

            cmd.ExecuteNonQuery();
            DatabaseHelper.CloseConnection();

            Storage.Delete(FileName(delObj, delObj.Id));
        }

        protected void Seralise(IWorkflowInstantiationCriteria saveObj)
        {
            using (var writer = Storage.GetWriter(FileName(saveObj, saveObj.Id)))
            {
                Formatter.Serialize(writer.BaseStream, saveObj);
                writer.Close();
            }
        }

        protected string FileName(IWorkflowInstantiationCriteria o, object id)
        {
            return "WorkflowInstantionCriteria_" + id + ".bin";
        }

    }
}
