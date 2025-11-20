using AuthLibrary.Contexts;
using AuthLibrary.Services;
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

        private async void RegisterButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;

            if (!IsPasswordCorrect(password) || !IsLoginCorrect(login))
            {
                MessageBox.Show("Данные не корректны");
                return;
            }

            var isUserRegistered = await _authService.RegistrateUserAsync(login, password);

            MessageBox.Show($"{(isUserRegistered ? 
                "Пользователь успешно зарегистрирован" : 
                "При регистрации произошла ошибка")}");
        }

        private static bool IsPasswordCorrect(string password)
            => (String.IsNullOrWhiteSpace(password) && password.Length <= 3) ? false : true;

        private static bool IsLoginCorrect(string login)
             => (String.IsNullOrWhiteSpace(login)) ? false : true;

    }
}