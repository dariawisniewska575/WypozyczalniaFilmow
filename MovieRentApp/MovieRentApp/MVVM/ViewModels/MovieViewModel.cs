using MovieRentApp.Core;
using MovieRentApp.Models;
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
        Task.Run(() => LoadMoviesAsync());
    }

    public void RemoveMovie(int movieId)
        => Task.Run(() => _repository.RemoveMovieAsync(movieId));

    public Movie AddMovie(Movie newMovie)
        => Task.Run(() => _repository.AddMovieAsync(newMovie)).Result;

    private async Task LoadMoviesAsync()
    {
        var movies = await _repository.GetMoviesAsync();
        Movies = new ObservableCollection<Movie>(movies);
    }
}
