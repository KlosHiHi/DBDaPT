using AuthLibrary.Contexts;
using AuthLibrary.Models;
using AuthLibrary.Services;
using ClientApp.Class;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthService _authService = new(new CinemaUserDbContext());
        public MainWindow()
        {
            InitializeComponent();

            SetWelcomeText();

            SetPrivileges();
        }

        private void SetWelcomeText()
        {
            WelcomeTextBlock.Text = $"{UserSession.Instance.CurrentUser.Login}, добро пожаловать!!!";
        }

        private async Task SetPrivileges()
        {
            var role = await _authService.GetUserRoleAsync(UserSession.Instance.CurrentUser.Login);

            switch (role.RoleName)
            {
                case "Администратор":
                    AddUserButton.Visibility = Visibility.Visible;
                    EditUserButton.Visibility = Visibility.Visible;
                    break;
                case "Билетер":
                    ShowTicketsButton.Visibility = Visibility.Visible;
                    ShowFilmsButton.Visibility = Visibility.Visible;
                    break;
                case "Посетитель":
                    ShowFilmsButton.Visibility = Visibility.Visible;
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

        private void NoImplementButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функционал ещё не реализован!");
        }
    }
}
