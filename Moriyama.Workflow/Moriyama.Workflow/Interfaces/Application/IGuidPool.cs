using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Application
{
    public interface IGuidPool
    {
        IEnumerable<string> CreateGuids(int count);
    }
}
