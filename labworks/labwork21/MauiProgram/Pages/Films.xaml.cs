using FilmLibrary.Contexts;
using FilmLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiProgram.Pages;

public partial class FilmsPage : ContentPage
{
    private FilmDbContext _context = new();
    public List<Film> Films { get; set; }
    public List<string> Sort { get; set; } = new()
    {
        "Название",
        "Длительность",
        "По году выпуска",
        "По убыванию года выпуска"
    };

    private string _searchText = "";
    private int _sortColumn = -1;

    private int _pageSize = 3;
    private int _pageIndex = 1;
    private int _one = 1;
    private int _totalPage;

    public FilmsPage()
    {
        InitializeComponent();

        ShowFilms(_sortColumn, _searchText);

        BindingContext = this;
    }

    private async void ShowFilms(int sort, string searchText)
    {
        var films = _context.Films.AsQueryable();

        if (!String.IsNullOrWhiteSpace(searchText))
        {
            films = films.Where(f => f.Name.Contains(searchText));
        }

        if (sort != -1)
        {
            films = sort switch
            {
                0 => films.OrderBy(f => f.Name),
                1 => films.OrderBy(f => f.Duration),
                2 => films.OrderBy(f => f.ReleaseYear),
                3 => films.OrderByDescending(f => f.ReleaseYear),
                _ => films
            };
        }

        films = Pagination(films);

        Films = await films.ToListAsync();
        FilmListView.ItemsSource = Films;
    }

    private async void FindButton_Clicked(object sender, EventArgs e)
    {
        _searchText = FilmNameEntry.Text;
        _pageIndex = 1;
        ShowFilms(_sortColumn, _searchText);
    }

    private async void SortPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        _sortColumn = SortPicker.SelectedIndex;
        _pageIndex = 1;
        ShowFilms(_sortColumn, _searchText);
    }

    private IQueryable<Film> Pagination(IQueryable<Film> films)
    {
        decimal page = (decimal)films.Count() / (decimal)_pageSize;

        _totalPage = (int)Math.Ceiling(page);

        if (_pageIndex <= _one)
            BackPageButton.IsEnabled = false;

        if (_pageIndex >= _totalPage)
            ForwardPageButton.IsEnabled = false;

        if (_pageIndex < _totalPage)
            ForwardPageButton.IsEnabled = true;

        if (_pageIndex > _one)
            BackPageButton.IsEnabled = true;

        if (_totalPage != 1)
            films = films.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize);

        PageNumberLabel.Text = _pageIndex.ToString();
        return films;
    }

    private void BackPageButton_Clicked(object sender, EventArgs e)
    {
        _pageIndex--;

        ShowFilms(_sortColumn, _searchText);
    }

    private void ForwardPageButton_Clicked(object sender, EventArgs e)
    {
        _pageIndex++;

        ShowFilms(_sortColumn, _searchText);
    }
}