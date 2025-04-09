using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class Project
    {
        public int ProjectId { get; set; } 
        public string Name { get; set; }

        public int UserId { get; set; } 
        public User User { get; set; } 

        public ICollection<Shape> Shapes { get; set; } = new List<Shape>();

        public Project()
        {
            
        }

        public Project(string name, int userId) : base()
        {
            Name = name;
            UserId = userId;
        }
    }
}