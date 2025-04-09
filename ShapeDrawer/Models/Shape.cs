namespace ShapeDrawer.Models
{
    public class Shape
    {
        public int ShapeId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Name { get; set; }

        public Shape(int x, int y, int projectId, string name)
        {
            X = x;
            Y = y;
            ProjectId = projectId;
            Name = name; 
        }
    }
}