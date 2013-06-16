using System.Collections.Generic;

namespace FergusonMoriyam.Workflow.Interfaces.Application
{
    public interface IGuidPool
    {
        IEnumerable<string> CreateGuids(int count);
    }
}
