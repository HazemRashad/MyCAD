using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;

namespace ShapeDrawer.ViewModels
{
    public class RectangleViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainViewModel;

        public ICommand AddCommand { get; }
        public ICommand CloseCommand { get; }

        private double _width;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _height;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
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
        public RectangleViewModel()
        {
            
        }

        public RectangleViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            AddCommand = new RelayCommand(Add);
            CloseCommand = new RelayCommand(Close);
        }

        private void Add()
        {
            // Add the rectangle to the Shapes collection in MainWindowViewModel
            var rectangle = new Rectangle((int)Width, (int)Height, (int)X, (int)Y, 1, $"Rectangle{_mainViewModel.Shapes.Count + 1}");
            _mainViewModel.Shapes.Add(rectangle);

            // Close the RectangleView window
            Close();
        }

        private void Close()
        {
            // Logic for closing the window
            Application.Current.Windows.OfType<RectangleView>().FirstOrDefault()?.Close();
        }
    }
}