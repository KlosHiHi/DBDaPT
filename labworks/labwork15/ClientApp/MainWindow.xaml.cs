using ClientApp.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WelcomeTextBlock.Text = $"{UserSession.Instance.CurrentUser.Login}, добро пожаловать!!!";
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.Instance.Clear();
            StartWindow window = new();
            window.Show();
            Close();
        }
    }
}
