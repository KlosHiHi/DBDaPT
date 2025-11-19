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
            MessageBox.Show($"{(await _authService.RegistrateUserAsync(LoginTextBox.Text, PasswordTextBox.Text) ? 
                "Пользователь успешно зарегистрирован" : 
                "При регистрации произошла ошибка")}");
        }
    }
}