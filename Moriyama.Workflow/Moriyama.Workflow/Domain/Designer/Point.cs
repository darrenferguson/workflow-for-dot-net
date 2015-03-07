using Moriyama.Workflow.Interfaces.Domain.Designer;

namespace Moriyama.Workflow.Domain.Designer
{
    public class Point : IPoint
    {
        public string OwnerId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
