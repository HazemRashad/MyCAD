using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;
using Microsoft.EntityFrameworkCore; // Required for Include

namespace ShapeDrawer.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        // Collection of shape types (for the ComboBox)
        public ObservableCollection<string> ShapeTypes { get; } = new ObservableCollection<string>
        {
            "Rectangle",
            "Circle",
            "Line"
        };

        // Collection of shapes (for the ListView)
        public ObservableCollection<Shape> Shapes { get; } = new ObservableCollection<Shape>();

        public int ProjectId { get; }
        public int UserId { get; }


        

        public MainWindowViewModel(int projectId, int userid) : base()
        {
            ProjectId = projectId;
            UserId = userid;
            LoadShapesForProject();
            AddEditCommand = new RelayCommand(AddEditShape);
            DeleteCommand = new RelayCommand(DeleteShape);
            ExitCommand = new RelayCommand(ExitApplication);
            OpenCommand = new RelayCommand(OpenRecentView);
            SelectedShapeType = ShapeTypes.FirstOrDefault();
        }

        private void LoadShapesForProject()
        {
            using (var context = new ShapeDrawerDbContext())
            {
                // Fetch the project and its shapes from the database
                var project = context.Projects
                    .Include(p => p.Shapes) // Ensure shapes are loaded
                    .FirstOrDefault(p => p.ProjectId == ProjectId); // Correct lambda expression

                if (project != null)
                {
                    // Clear the existing shapes and add the fetched shapes
                    Shapes.Clear();
                    foreach (var shape in project.Shapes)
                    {
                        Shapes.Add(shape);
                    }
                }
            }
        }

        // Method to update the Shapes collection
        public void UpdateShapes(IEnumerable<Shape> newShapes)
        {
            Shapes.Clear(); // Clear the existing shapes
            foreach (var shape in newShapes)
            {
                Shapes.Add(shape); // Add the new shapes
            }
        }

        // Selected shape type in the ComboBox
        private string _selectedShapeType;
        public string SelectedShapeType
        {
            get => _selectedShapeType;
            set => SetProperty(ref _selectedShapeType, value);
        }

        // Selected shape in the ListView
        private Shape _selectedShape;
        public Shape SelectedShape
        {
            get => _selectedShape;
            set => SetProperty(ref _selectedShape, value);
        }

        // Commands
        public ICommand AddEditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand OpenCommand { get; }



        public MainWindowViewModel()
        {
           
        }

        private void AddEditShape()
        {
            if (string.IsNullOrEmpty(SelectedShapeType))
            {
                MessageBox.Show("Please select a shape type to add.");
                return;
            }

            // Open the corresponding shape view based on the selected shape type
            switch (SelectedShapeType)
            {
                case "Rectangle":
                    var rectangleView = new RectangleView { DataContext = new RectangleViewModel(this) };
                    rectangleView.Show();
                    break;

                case "Circle":
                    var circleView = new CircleView { DataContext = new CircleViewModel(this) };
                    circleView.Show();
                    break;

                case "Line":
                    var lineView = new LineView { DataContext = new LineViewModel(this) }; // Pass 'this' (MainWindowViewModel)
                    lineView.Show();
                    break;

                default:
                    MessageBox.Show("Unknown shape type selected.");
                    break;
            }
        }

        private void DeleteShape()
        {
            if (SelectedShape == null)
            {
                MessageBox.Show("Please select a shape to delete.");
                return;
            }

            Shapes.Remove(SelectedShape);
            MessageBox.Show("Shape deleted.");
        }

        // Command for exiting the application
        private void ExitApplication()
        {
            Application.Current.Shutdown(); // Shuts down the application
        }

        private void OpenRecentView()
        {

            // Assuming RecentView is a Window or Page
            var recentView = new Recent(UserId);
            recentView.Show();

        }
    }
} 