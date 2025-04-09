using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ShapeDrawer.ViewModels
{
    public partial class RecentViewModel : ObservableObject
    {


        private ObservableCollection<Project> recentprojects;
        public ObservableCollection<Project> RecentProjects
        {
            get => recentprojects;
            set => SetProperty(ref recentprojects, value);
        }



        
        private Project selectedproject;
        public Project SelectedProject
        {
            get => selectedproject;
            set => SetProperty(ref selectedproject, value); 
        }

        public int CurrentUserId { get; } // Store the current user ID

        public ICommand OpenProjectCommand { get; }
        public ICommand NewProjectCommand { get; }
        //public RecentViewModel()
        //{

        //}

        public RecentViewModel(int userId) : base()
        {
            RecentProjects = new ObservableCollection<Project>();
            CurrentUserId = userId; // Set the current user ID
            LoadRecentProjects();
            OpenProjectCommand = new RelayCommand(OpenProject);
            NewProjectCommand = new RelayCommand(NewProject);
        }

        private void LoadRecentProjects()
        {
            using (var context = new ShapeDrawerDbContext())
            {
                // Fetch projects for the current user
                var projects = context.Projects
                    .Where(p => p.UserId == CurrentUserId) // Use CurrentUserId
                    .Take(10) // Limit to 10 most recent projects
                    .ToList();

                // Clear the existing list and add the fetched projects
                RecentProjects.Clear();
                foreach (var project in projects)
                {
                    RecentProjects.Add(project);
                }
            }
        }


        private void OpenProject()
        {
            if (SelectedProject == null)
            {
                MessageBox.Show("Please select a project to open.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Open the selected project in the main window
            var mainWindow = new MainWindow();
            if (mainWindow.DataContext is MainWindowViewModel mainViewModel)
            {
                // Pass the ProjectId to the MainWindowViewModel
                mainViewModel = new MainWindowViewModel(SelectedProject.ProjectId, CurrentUserId);
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show(); // Show the MainWindow
                CloseRecentWindow(); // Close the Recent window
            }
            else
            {
                MessageBox.Show("Failed to open the MainWindow.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void NewProject()
        {
            // Open the AddProject window
            var addProjectWindow = new AddProject();
            addProjectWindow.DataContext = new AddProjectViewModel(this); // Pass the RecentViewModel
            addProjectWindow.ShowDialog(); // Show as a dialog
        }

        private void CloseRecentWindow()
        {
            var recentWindow = Application.Current.Windows.OfType<Recent>().FirstOrDefault();
            recentWindow?.Close(); // Close the Recent window
        }
    }
}