using System.Collections.Generic;
using System.Reflection;
using Moriyama.Workflow.Domain.Designer;
using Moriyama.Workflow.Infrastructure;
using Moriyama.Workflow.Interfaces.Application;
using Moriyama.Workflow.Interfaces.Domain.Designer;
using Moriyama.Workflow.Interfaces.Infrastructure;
using log4net;

namespace Moriyama.Workflow.Application
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
