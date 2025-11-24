using AuthLibrary.Contexts;
using AuthLibrary.Models;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        CinemaUserDbContext _context = new();

        public UserWindow()
        {
            InitializeComponent();

            var users = _context.CinemaUsers;
            UserDataGrid.ItemsSource = users.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            foreach (CinemaUser user in UserDataGrid.ItemsSource)
                _context.CinemaUsers.Update(user);

            _context.SaveChanges();
        }
    }
}
