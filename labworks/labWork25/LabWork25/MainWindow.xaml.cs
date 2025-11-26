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
    public partial class MainWindow : Window
    {
        FilmService _service = new FilmService(new CinemaDbContext());

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
            var filmDtos = await _service.GetFilmsDtosAsync();

            FilmDataGrid.ItemsSource = filmDtos;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new() 
            { 
                Filter = "CSV files (*.csv)|*.csv|XLSX files (*.xlsx)|*.xlsx"
            };

            var result = saveFileDialog.ShowDialog();

            var fileName = saveFileDialog.FileName;

            using (StreamWriter writer = new(fileName))
            {
                writer.WriteLine("Название;Время начала сеанса;Кинотеатр;Номер зала;Цена");

                var films = FilmDataGrid.Items.OfType<FilmDto>().ToList();

                foreach (var film in films)
                {
                    writer.WriteLine($"{film.Name};{film.StartDate};{film.CinemaName};{film.HallNumber};{film.Price}");
                }
            }
        }

        private void ShowFilmsByDateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowAllFilmsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}