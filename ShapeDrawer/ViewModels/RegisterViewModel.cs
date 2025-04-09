using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShapeDrawer.Models;
using ShapeDrawer.Views;

namespace ShapeDrawer.ViewModels
{
    internal class RegisterViewModel : ObservableObject
    {
        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value); // Notify property change
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value); // Notify property change
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value); // Notify property change
        }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Register()
        {
            // Debug: Check the values of Password and ConfirmPassword
            Debug.WriteLine($"Password: {Password}");
            Debug.WriteLine($"ConfirmPassword: {ConfirmPassword}");

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Password and Confirm Password cannot be empty.");
                return;
            }

            if (Password == ConfirmPassword)
            {
                // Hash the password before saving
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

                // Create a new User instance using the parameterized constructor
                var user = new User(Username, hashedPassword);

                // Save the user to the database
                using (var context = new ShapeDrawerDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                MessageBox.Show("Registration successful!");
                var loginWindow = new Login();
                loginWindow.Show();

                // Close the Register window
                Application.Current.Windows.OfType<Register>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Passwords do not match.");
            }
        }

        private void Cancel()
        {
            var loginWindow = new Login();
            loginWindow.Show();

            // Close the Register window
            Application.Current.Windows.OfType<Register>().FirstOrDefault()?.Close();
        }
    }
}