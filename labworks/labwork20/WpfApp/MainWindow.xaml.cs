using FilmLibrary.Contexts;
using FilmLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilmDbContext _context = new();
        private FileInfo _fileInfo;

        public MainWindow()
        {
            InitializeComponent();

            FilmNameComboBox.ItemsSource = _context.Films.Select(f => f.Name).ToList();
        }

        private void SelectFrameButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Filter = "PNG (*png)| *.png | JPEG(*.jpeg) | *.jpeg",
                Title = "Выбор кадра",
                Multiselect = false
            };

            if(!dialog.ShowDialog().Value)
                return;

            _fileInfo = new(dialog.FileName);
            FileNameTextBlock.Text = _fileInfo.Name;

            _fileInfo.CopyTo(Path.Combine(Directory.GetCurrentDirectory(), "Images", _fileInfo.Name), true);

            Uri LogoUri = new(dialog.FileName);
            ImageSource LogoImage = new BitmapImage(LogoUri);
            FilmFrameImage.Source = LogoImage;
        }

        private async void SaveFrame_Click(object sender, RoutedEventArgs e)
        {
            var film = await _context.Films.FirstOrDefaultAsync(f => f.Name == FilmNameComboBox.SelectedItem.ToString());

            if (film is null)
                MessageBox.Show("При cохранении произошла ошибка");

            Frame frame = new()
            {
                FileName = _fileInfo.Name,
                FilmId = film.FilmId
            };

            try
            {
                await _context.Frames.AddAsync(frame);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}