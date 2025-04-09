using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ShapeDrawer.ViewModels
{
    public partial class AddProjectViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        
        public ICommand AddProjectCommand { get; }


        

        private readonly RecentViewModel _recentViewModel;

        public AddProjectViewModel()
        {
            
        }

        public AddProjectViewModel(RecentViewModel recentViewModel) : base()
        {
            _recentViewModel = recentViewModel;
            AddProjectCommand = new RelayCommand(AddProject);
        }



        private void AddProject()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Please enter a project name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get the current user ID from the RecentViewModel
            int currentUserId = _recentViewModel.CurrentUserId;

            // Check if a project with the same name already exists for this user
            using (var context = new ShapeDrawerDbContext())
            {
                bool projectExists = context.Projects
                    .Any(p => p.Name == Name && p.UserId == currentUserId);

                if (projectExists)
                {
                    MessageBox.Show("A project with this name already exists for the current user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Exit the function if the project name already exists
                }
            }

            // Create a new project with the current user ID
            var project = new Project(Name, currentUserId);

            // Add the project to the database
            using (var context = new ShapeDrawerDbContext())
            {
                context.Projects.Add(project); // Add the project to the DbSet
                context.SaveChanges(); // Save changes to the database
            }

            // Add the project to the RecentProjects list in RecentViewModel
            _recentViewModel.RecentProjects.Add(project);

            // Show success message
            MessageBox.Show($"Project '{Name}' added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Close the window
            Application.Current.Windows.OfType<AddProject>().FirstOrDefault()?.Close();
        }
    }
}