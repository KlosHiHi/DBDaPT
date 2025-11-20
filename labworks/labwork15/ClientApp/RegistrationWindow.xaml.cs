using AuthLibrary.Contexts;
using AuthLibrary.Services;
using ClientApp.Class;
using System.Windows;

namespace ClientApp
{
    public partial class RegistrationWindow : Window
    {
        private AuthService _authService = new(new CinemaUserDbContext());

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private static bool IsPasswordCorrect(string password)
            => (String.IsNullOrWhiteSpace(password) && password.Length <= 3) ? false : true;

        private static bool IsLoginCorrect(string login)
             => (String.IsNullOrWhiteSpace(login)) ? false : true;

        private async void RegisterButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!IsPasswordCorrect(PasswordTextBox.Text) || !IsLoginCorrect(LoginTextBox.Text))
            {
                MessageBox.Show("Данные не корректны");
                return;
            }

            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;

            var isUserRegistered = await _authService.RegistrateUserAsync(login, password);

            MessageBox.Show($"{(isUserRegistered ?
                "Пользователь успешно зарегистрирован" :
                "При регистрации произошла ошибка")}");

            if (isUserRegistered)
            {
                await StartNewSession(login, password);

                MainWindow window = new();
                window.ShowDialog();
                Close();
            }
        }

        private async Task StartNewSession(string login, string password)
        {
            var user = await _authService.AuthUserAsync(login, password);
            UserSession.Instance.SetCurrentUser(user);
        }
    }
}