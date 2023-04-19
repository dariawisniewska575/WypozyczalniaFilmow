using MovieRentApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentApp.Repository;

public interface IRepository
{
    Task<Movie> AddMovieAsync(Movie newMovie);
    Task EditMovieAsync(Movie movieToEdit);
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task RemoveMovieAsync(int movieId);
}