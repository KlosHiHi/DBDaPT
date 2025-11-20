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

            if (!IsPasswordCorrect(password) || !IsLoginCorrect(login))
            {
                MessageBox.Show("Данные не корректны");
                return;
            }

            await StartNewSessionAsync(login, password);

            OpenMainWindow();
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
            UserSession.Instance.SetCurrentUser(user);
        }
        private static bool IsPasswordCorrect(string password)
            => (String.IsNullOrWhiteSpace(password)) ? false : true;

        private static bool IsLoginCorrect(string login)
             => (String.IsNullOrWhiteSpace(login)) ? false : true;
    }
}
