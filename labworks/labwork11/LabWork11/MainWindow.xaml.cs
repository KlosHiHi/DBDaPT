using LabWork11.Context;
using LabWork11.Model;
using LabWork11.Services;
using System.Windows;

namespace LabWork11
{
    public partial class MainWindow : Window
    {
        private Ispp3101Context _context;
        private FilmService _filmService;

        public MainWindow()
        {
            InitializeComponent();

            _context = new Ispp3101Context();
            _filmService = new FilmService(_context);


            InitializeData();
        }

        private async Task InitializeData()
        {

            FilmDataGrid.ItemsSource = await _filmService.GetAsync();
        }

        private async void AddButton_ClickAsync(object sender, RoutedEventArgs e)
        {

        }

        private async void RemoveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (IsDeleteRow())
                await RemoveFilmAsync();
        }

        private bool IsDeleteRow()
        {
            var result = MessageBox.Show($"Вы уверены, что хотите удалить {FilmDataGrid.SelectedItems.Count} записей", "Удаление", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                return true;

            return false;
        }

        private async Task RemoveFilmAsync()
        {
            try
            {
                await _filmService.RemoveAsync(FilmDataGrid.SelectedItems.Cast<Film>().ToList());
                MessageBox.Show("Данные успешно удалены", "Информация", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось удалить записи. Причина: {ex.Message}", "Ошибка", MessageBoxButton.OK);
            }
            finally
            {
                await InitializeData();
            }
        }
    }
}