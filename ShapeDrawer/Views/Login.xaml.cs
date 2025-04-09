using System.Windows;
using ShapeDrawer.ViewModels;

namespace ShapeDrawer.Views
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        //public bool LoginWasSuccessful => ((LoginViewModel)DataContext).LoginWasSuccessful;
    }
}