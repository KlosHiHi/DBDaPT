using GameStoreDbLibrary.Contexts;
using GameStoreDbLibrary.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabWork23
{
    public partial class MainWindow : Window
    {
        private GamesService _service = new(new GameStoreDbContext());
        private FileInfo _fileInfo;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await SetData();
        }

        private async Task SetData()
        {
            var gamesName = await _service.GetGamesNameAsync();

            GamesComboBox.ItemsSource = gamesName;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await _service.TryChangeLogoFile(_fileInfo.Name, GamesComboBox.SelectedItem.ToString());

            if (result)
                MessageBox.Show("Изменения успешно применены");
            else
                MessageBox.Show("При сохранение произошла ошибка");
        }

        private void SelectLogoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Filter = "",
                Title = "Выберите логотип",
                Multiselect = false
            };

            if (!dialog.ShowDialog().Value)
                return;

            _fileInfo = new(dialog.FileName);
            FileNameTextBlock.Text = _fileInfo.Name;

            _fileInfo.CopyTo(Path.Combine(Environment.CurrentDirectory, "Logos", _fileInfo.Name), true);

            Uri LogoUri = new(dialog.FileName);
            ImageSource LogoImage = new BitmapImage(LogoUri);
            ShowLogoImage.Source = LogoImage;
        }

        private void GamesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FileNameTextBlock.Text = "";
        }
    }
}