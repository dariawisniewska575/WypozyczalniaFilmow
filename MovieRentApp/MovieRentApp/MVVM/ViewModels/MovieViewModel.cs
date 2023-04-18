using MovieRentApp.Core;
using MovieRentApp.Models;
using MovieRentApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MovieRentApp.MVVM.ViewModels;

public class MovieViewModel : ObservableObject
{
    private ObservableCollection<Movie>? _movies;
    protected readonly IRepository _repository;
    public ObservableCollection<Movie> Movies
    {
        get { return _movies; }
        set
        {
            _movies = value;
            OnPropertyChanged();
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
        => Task.Run(() => _repository.RemoveMovie(movieId));

    private async Task LoadMoviesAsync()
    {
        var movies = await _repository.GetMoviesAsync();
        Movies = new ObservableCollection<Movie>(movies);
    }
}
