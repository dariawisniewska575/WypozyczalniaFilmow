using MovieRentApp.Core;
using MovieRentApp.MVVM.Models;
using MovieRentApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MovieRentApp.MVVM.ViewModels;

public class MovieViewModel : ObservableObject
{
    private readonly IRepository _repository;

    private ObservableCollection<Movie>? _movies;
    public ObservableCollection<Movie> Movies
    {
        get { return _movies; }
        set
        {
            _movies = value;
            OnPropertyChanged();
        }
    }

    private bool _isEditOrDeleteButtonEnabled;
    public bool IsEditOrDeleteButtonEnabled
    {
        get { return _isEditOrDeleteButtonEnabled; }
        set
        {
            _isEditOrDeleteButtonEnabled = value;
            OnPropertyChanged(nameof(IsEditOrDeleteButtonEnabled));
        }
    }

    private Movie _selectedItem;
    public Movie SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            IsEditOrDeleteButtonEnabled = _selectedItem != null;
        }
    }

    public MovieViewModel()
    {
        
    }

    public MovieViewModel(IRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        LoadMovies();
    }

    public void RemoveMovie(Movie movie)
        => Task.Run(() => _repository.DeleteEntityAsync(movie));

    public Movie AddMovie(Movie newMovie)
        => Task.Run(() => _repository.AddEntityAsync(newMovie)).Result;

    public void EditMovie(Movie movieToEdit)
        => Task.Run(() => _repository.EditEntityAsync(movieToEdit));

    public async Task LoadMoviesAsync()
    {
        var movies = await _repository.GetEntitiesAsync<Movie>();
        Movies = new ObservableCollection<Movie>(movies);
    }

    private void LoadMovies()
    {
        var movies = _repository.GetEntities<Movie>();
        Movies = new ObservableCollection<Movie>(movies);
    }
}
