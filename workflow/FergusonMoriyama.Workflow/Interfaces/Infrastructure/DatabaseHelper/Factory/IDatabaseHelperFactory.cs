namespace FergusonMoriyam.Workflow.Interfaces.Infrastructure.DatabaseHelper.Factory
{
    public interface IDatabaseHelperFactory
    {
        IDatabaseHelper CreateDatabaseHelper(string assemblyQualifiedName);
    }
}
