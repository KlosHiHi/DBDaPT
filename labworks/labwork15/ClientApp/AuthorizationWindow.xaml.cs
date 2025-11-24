using AuthLibrary.Contexts;
using AuthLibrary.Services;
using ClientApp.Class;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private AuthService _authService = new(new CinemaUserDbContext());

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private async void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;

            await StartNewSessionAsync(login, password);

        }

        private void OpenMainWindow()
        {
            MainWindow window = new();
            window.Show();
            Close();
        }

        private async Task StartNewSessionAsync(string login, string password)
        {
            var user = await _authService.AuthUserAsync(login, password);

            if (user is null)
            {
                MessageBox.Show("Данные не верны");
                return;
            }

            OpenMainWindow();
            UserSession.Instance.SetCurrentUser(user);
        }
    }
}
