using CinemaClassLibrary.DTOs;
using CinemaClassLibrary.Services;
using CinemaDbLibrary.Contexts;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace LabWork25
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FilmsWindows : Window
    {
        FilmService _service = new(new CinemaDbContext());

        public FilmsWindows()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await SetData();
        }

        private async Task SetData()
        {
            var filmDtos = await _service.GetFilmsDtosAsync();
            var cinemaNames = await _service.GetCinemaNamesAsync();
                       
            CinemaNameComboBox.ItemsSource = cinemaNames;            
            FilmDataGrid.ItemsSource = filmDtos;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Сохранить таблицу"
            };

            if (saveFileDialog.ShowDialog() != true)
                return;

            var fileName = saveFileDialog.FileName;

            ExportTableFiles(fileName);
        }

        private void ExportTableFiles(string fileName)
        {
            using (StreamWriter writer = new(fileName, false, System.Text.Encoding.Unicode))
            {
                writer.WriteLine("Название;Время начала сеанса;Кинотеатр;Номер зала;Цена");
                var films = FilmDataGrid.Items.OfType<FilmDto>().ToList();

                foreach (var film in films)
                    writer.WriteLine($"{film.Name};{film.StartDate};{film.CinemaName};{film.HallNumber};{film.Price}");
            }
        }

        private async void ShowFilteredFilmsButton_Click(object sender, RoutedEventArgs e)
        {
            var date = FilmsDatePicker.SelectedDate ?? DateTime.Now;
            var cinemaName = CinemaNameComboBox.Text ?? "---";

            var filteredFilms = await _service.GetFilteredFilmDto(date, cinemaName);

            FilmDataGrid.ItemsSource = filteredFilms;
        }

        private async void ShowAllFilmsButton_Click(object sender, RoutedEventArgs e)
        {
            var filmDtos = await _service.GetFilmsDtosAsync();

            FilmDataGrid.ItemsSource = filmDtos;
        }
    }
}