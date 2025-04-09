using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class User
    {
        public int UserId { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}