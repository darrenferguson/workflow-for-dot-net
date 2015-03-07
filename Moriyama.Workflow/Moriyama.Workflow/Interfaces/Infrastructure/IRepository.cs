using System.Collections.Generic;

namespace Moriyama.Workflow.Interfaces.Infrastructure
{
    public interface IRepository<TD, in TK>
    {
        IList<TD> List();
        TD GetById(TK id);
        TD RestoreState(TD obj);

        void Create(TD saveObj);
        void Update(TD saveObj);
        void Delete(TD delObj);
    }
}
