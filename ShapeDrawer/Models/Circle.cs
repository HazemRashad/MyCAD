namespace ShapeDrawer.Models
{
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius, int x, int y, int projectId, string name)
            : base(x, y, projectId, name)
        {
            Radius = radius;
        }
    }
}