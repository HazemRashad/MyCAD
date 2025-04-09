using System.Windows;
using ShapeDrawer.ViewModels;

namespace ShapeDrawer.Views
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(); // Set the DataContext
        }
    }
}