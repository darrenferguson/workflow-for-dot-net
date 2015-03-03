using System.Collections.Generic;
using System.Reflection;
using FergusonMoriyam.Workflow.Domain.Designer;
using FergusonMoriyam.Workflow.Infrastructure;
using FergusonMoriyam.Workflow.Interfaces.Application;
using FergusonMoriyam.Workflow.Interfaces.Domain.Designer;
using FergusonMoriyam.Workflow.Interfaces.Infrastructure;
using log4net;

namespace FergusonMoriyam.Workflow.Application
{
    public class PointService : IPointService
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly PointService Service = new PointService();
        
        public static PointService Instance
        {
            get { return Service; }
        }

        private PointService()
        {
            ServiceWorkflowDesignerPointRespository = WorkflowDesignerPointRespository.Instance;
        }
        #endregion

        #region properties
        public IDesignerPointRepository<IPoint, string> ServiceWorkflowDesignerPointRespository { get; set; }
        #endregion

        public void DeletePoints(IEnumerable<string> pointIds)
        {
            ServiceWorkflowDesignerPointRespository.Delete(pointIds);
        }

        public void AddPoint(string pointId, int top, int left)
        {
            ServiceWorkflowDesignerPointRespository.Create(new Point {OwnerId = pointId, X = left, Y = top});
        }

        public IEnumerable<IPoint> GetPoints(IEnumerable<string> pointIds)
        {
            return ServiceWorkflowDesignerPointRespository.List(pointIds);
        }
    }
}
