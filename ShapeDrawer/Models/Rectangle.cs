namespace ShapeDrawer.Models
{
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height, int x, int y, int projectId, string name)
            : base(x, y, projectId, name)
        {
            Width = width;
            Height = height;
        }
    }
}