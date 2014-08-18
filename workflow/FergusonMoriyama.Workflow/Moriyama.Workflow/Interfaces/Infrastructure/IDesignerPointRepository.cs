using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Infrastructure
{
    public interface IDesignerPointRepository<TD, in TK>
    {
        IList<TD> List(TK id);
        IList<TD> List(IEnumerable<TK> ids);
        void Create(TD saveObj);
        void Delete(TK id);
        void Delete(IEnumerable<TK> ids);
    }
}
