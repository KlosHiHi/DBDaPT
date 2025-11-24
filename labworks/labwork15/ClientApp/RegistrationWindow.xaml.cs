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

            await RegistrateUser(login, password);
        }

        private async Task RegistrateUser(string login, string password)
        {
            var isUserRegistered = await _authService.RegistrateUserAsync(login, password);

            MessageBox.Show($"{(isUserRegistered ?
                "Пользователь успешно зарегистрирован" :
                "При регистрации произошла ошибка")}");
        }
    }
}