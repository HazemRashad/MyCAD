using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;

namespace ShapeDrawer.ViewModels
{
    internal class LineViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainViewModel;

        public ICommand AddCommand { get; }
        public ICommand CloseCommand { get; }

        private double _x1;
        public double X1
        {
            get => _x1;
            set => SetProperty(ref _x1, value);
        }

        private double _y1;
        public double Y1
        {
            get => _y1;
            set => SetProperty(ref _y1, value);
        }

        private double _x2;
        public double X2
        {
            get => _x2;
            set => SetProperty(ref _x2, value);
        }

        private double _y2;
        public double Y2
        {
            get => _y2;
            set => SetProperty(ref _y2, value);
        }

        public LineViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            AddCommand = new RelayCommand(Add);
            CloseCommand = new RelayCommand(Close);
        }

        private void Add()
        {
            // Add the line to the Shapes collection in MainWindowViewModel
            var line = new Line((int)X1, (int)Y1, (int)X2, (int)Y2, 1, $"Line{_mainViewModel.Shapes.Count + 1}");
            _mainViewModel.Shapes.Add(line);

            // Close the LineView window
            Close();
        }

        private void Close()
        {
            // Logic for closing the window
            Application.Current.Windows.OfType<LineView>().FirstOrDefault()?.Close();
        }
    }
}