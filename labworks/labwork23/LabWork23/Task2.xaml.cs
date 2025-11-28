using GameStoreDbLibrary.Contexts;
using GameStoreDbLibrary.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabWork23
{
    /// <summary>
    /// Логика взаимодействия для Task2.xaml
    /// </summary>
    public partial class Task2 : Window
    {
        private GamesService _service = new(new GameStoreDbContext());
        private FileInfo _fileInfo;
        private int _maxSize = 2_097_152;
        public Task2()
        {
            InitializeComponent();
        }

        private void SelectScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Filter = "",
                Title = "Выберите скриншот",
                Multiselect = false
            };

            if (!dialog.ShowDialog().Value)
                return;

            _fileInfo = new(dialog.FileName);
            FileNameTextBlock.Text = _fileInfo.Name;

            Uri LogoUri = new(dialog.FileName);
            ImageSource LogoImage = new BitmapImage(LogoUri);
            ShowScreenShotImage.Source = LogoImage;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] fileBytes = File.ReadAllBytes(_fileInfo.FullName);
            if (fileBytes.Length > _maxSize)
            {
                MessageBox.Show("Файл слишком большой");
                return;
            }

            var result = await _service.TryUploadScreenshot(GamesComboBox.SelectedItem.ToString(), _fileInfo.Name, fileBytes);

            if (result)
                MessageBox.Show("Изменения успешно применены");
            else
                MessageBox.Show("При сохранение произошла ошибка");
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
    }
}
