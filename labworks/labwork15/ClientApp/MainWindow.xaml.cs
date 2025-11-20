using AuthLibrary.Contexts;
using AuthLibrary.Models;
using AuthLibrary.Services;
using ClientApp.Class;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthService _authService = new(new CinemaUserDbContext());
        private CinemaUserRole _userRole;

        public MainWindow()
        {
            InitializeComponent();
            WelcomeTextBlock.Text = $"{UserSession.Instance.CurrentUser.Login}, добро пожаловать!!!";

            SetPrivileges();
        }


        private void SetPrivileges()
        {
            var roleId = UserSession.Instance.CurrentUser.RoleId;

            switch (roleId)
            {
                case 1:
                    AddUserButton.Visibility = Visibility.Visible;
                    ToEditUserWindowButton.Visibility = Visibility.Visible;
                    break;
                case 2:
                    ShowTicketButton.Visibility = Visibility.Visible;
                    ShowFilmButton.Visibility = Visibility.Visible;
                    break;
                case 3:
                    ShowFilmButton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.Instance.Clear();
            OpenStartWindow();
        }

        private void OpenStartWindow()
        {
            StartWindow window = new();
            window.Show();
            Close();
        }

        private void ToEditUserWindowButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new();
            Hide();
            window.ShowDialog();
            Show();
        }
    }
}
