using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;

namespace ShapeDrawer.ViewModels
{
    public class CircleViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainViewModel;

        public ICommand AddCommand { get; }
        public ICommand CloseCommand { get; }

        private double _radius;
        public double Radius
        {
            get => _radius;
            set => SetProperty(ref _radius, value);
        }

        private double _x;
        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private double _y;
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        public CircleViewModel()
        {
            
        }
        public CircleViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            AddCommand = new RelayCommand(Add);
            CloseCommand = new RelayCommand(Close);
        }

        private void Add()
        {
            // Add the circle to the Shapes collection in MainWindowViewModel
            var circle = new Circle((int)Radius, (int)X, (int)Y, 1, $"Circle{_mainViewModel.Shapes.Count + 1}");
            _mainViewModel.Shapes.Add(circle);

            // Close the CircleView window
            Close();
        }

        private void Close()
        {
            // Logic for closing the window
            Application.Current.Windows.OfType<CircleView>().FirstOrDefault()?.Close();
        }
    }
}