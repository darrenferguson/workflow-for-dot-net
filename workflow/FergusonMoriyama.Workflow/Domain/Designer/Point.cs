using FergusonMoriyam.Workflow.Interfaces.Domain.Designer;

namespace FergusonMoriyam.Workflow.Domain.Designer
{
    public class Point : IPoint
    {
        public string OwnerId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
