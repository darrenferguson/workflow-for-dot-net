using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using FergusonMoriyam.Workflow.Domain;
using FergusonMoriyam.Workflow.Interfaces.Domain;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using log4net;


namespace FergusonMoriyam.Workflow.Infrastructure
{
    public class WorkflowConfigurationRepository : Repository, IRepository<IWorkflowConfiguration, int>
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Instance
        private static readonly WorkflowConfigurationRepository Repository = new WorkflowConfigurationRepository();

        public static WorkflowConfigurationRepository Instance
        {
            get { return Repository; }
        }

        private WorkflowConfigurationRepository()
        {
        }
        #endregion

        protected string FileName(IWorkflowConfiguration o, object id)
        {
            return "WorkflowConfigurationRepository_" + id + ".bin";
        }

        public IList<IWorkflowConfiguration> List()
        {
            var cmd = DatabaseHelper.CreateCommand("select * from workflowconfiguration");

            var configs = new List<IWorkflowConfiguration>();
            var records = cmd.ExecuteReader();

            while(records.Read())
            {
                configs.Add(
                    new WorkflowConfiguration
                        {
                            Id = (int) records["Id"],
                            Name = (string) records["Name"],
                            IsConfigurationActive = Convert.ToBoolean(records["IsConfigurationActive"]),
                            TypeName =  (string) records["TypeName"],
                            IsLocked = Convert.ToBoolean(records["Locked"])
                            
                        }
                );
            }
            DatabaseHelper.CloseConnection();
            return configs;
        }

        public IWorkflowConfiguration GetById(int id)
        {
           
            var cmd = DatabaseHelper.CreateCommand("select * from workflowconfiguration where id = @Id");
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", id));

            var records = cmd.ExecuteReader();
            records.Read();
            
            var c =  new WorkflowConfiguration
                       {
                           Id = (int) records["Id"],
                           Name = (string) records["Name"],
                           IsConfigurationActive = Convert.ToBoolean(records["IsConfigurationActive"]),
                           TypeName = (string) records["TypeName"],
                           IsLocked = Convert.ToBoolean(records["Locked"])
                       };
            DatabaseHelper.CloseConnection();
            return c;
        }

        public IWorkflowConfiguration RestoreState(IWorkflowConfiguration obj)
        {
            using (var reader = Storage.GetReader(FileName(obj, obj.Id)))
            {
                return (IWorkflowConfiguration) Formatter.Deserialize(reader.BaseStream);
            }
        }

        public void Create(IWorkflowConfiguration saveObj)
        {
            var transaction = DatabaseHelper.BeginTransaction(); 
            
            try {

                var cmd = DatabaseHelper.CreateCommand("insert into workflowconfiguration (name, isconfigurationactive, typename) values (@Name, @Active, @Type)");
                cmd.Transaction = transaction;

                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Name", saveObj.Name));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Active", saveObj.IsConfigurationActive));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Type", saveObj.GetType().FullName));
                cmd.Parameters.Add(DatabaseHelper.CreateParameter("Locked", saveObj.IsLocked));
                

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
                throw (sqlError);
            }

            DatabaseHelper.CloseConnection();
        }

        public void Update(IWorkflowConfiguration saveObj)
        {
            
            var cmd = DatabaseHelper.CreateCommand("update workflowconfiguration set name = @Name, isconfigurationactive = @Active, typename = @Type where id = @Id");

            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Name", saveObj.Name));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Active", saveObj.IsConfigurationActive));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Type", saveObj.GetType().FullName));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Locked", saveObj.IsLocked));
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", saveObj.Id));

            cmd.ExecuteNonQuery();

            DatabaseHelper.CloseConnection();

            Seralise(saveObj);
        }

        protected void Seralise(IWorkflowConfiguration saveObj)
        {
            using (var writer = Storage.GetWriter(FileName(saveObj, saveObj.Id)))
            {
                Formatter.Serialize(writer.BaseStream, saveObj);
                writer.Close();
            }
        }
        
        public void Delete(IWorkflowConfiguration delObj)
        {

            var cmd = DatabaseHelper.CreateCommand("delete from workflowconfiguration where id = @Id");
            cmd.Parameters.Add(DatabaseHelper.CreateParameter("Id", delObj.Id));

            cmd.ExecuteNonQuery();
            
            Storage.Delete(FileName(delObj, delObj.Id));
            DatabaseHelper.CloseConnection();
        }
    }
}
