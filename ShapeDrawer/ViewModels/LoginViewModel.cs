using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;

namespace ShapeDrawer.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(NavigateToRegister);
        }

        private void Login()
        {
            var user = ValidateUser(Username, Password);
            if (user != null)
            {
                // Open the Recent window with the current user's ID
                var recentViewModel = new RecentViewModel(user.UserId);
                var recentWindow = new Recent (user.UserId) { DataContext = recentViewModel };
                recentWindow.Show();

                // Close the login window
                Application.Current.Windows.OfType<Login>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void NavigateToRegister()
        {
            // Open RegisterView
            var registerView = new Register();
            registerView.Show(); // Show the Register window first

            // Close the login window after the Register window is shown
            Application.Current.Windows.OfType<Login>().FirstOrDefault()?.Close();
        }

        private User ValidateUser(string username, string password)
        {
            using (var context = new ShapeDrawerDbContext())
            {
                // Find the user by username
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user; // Return the user if validation is successful
                }

                return null; // User not found or password incorrect
            }
        }
    }
}