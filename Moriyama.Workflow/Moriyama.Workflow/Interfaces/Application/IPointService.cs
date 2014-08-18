using System.Collections.Generic;
using Moriyama.Workflow.Interfaces.Domain.Designer;

namespace Moriyama.Workflow.Interfaces.Application
{
    public interface IPointService
    {
        void DeletePoints(IEnumerable<string> pointIds);
        void AddPoint(string pointId, int top, int left);
        IEnumerable<IPoint> GetPoints(IEnumerable<string> pointIds);
    }
}
