using System.Collections.Generic;
using FergusonMoriyam.Workflow.Interfaces.Domain.Designer;

namespace FergusonMoriyam.Workflow.Interfaces.Application
{
    public interface IPointService
    {
        void DeletePoints(IEnumerable<string> pointIds);
        void AddPoint(string pointId, int top, int left);
        IEnumerable<IPoint> GetPoints(IEnumerable<string> pointIds);
    }
}
