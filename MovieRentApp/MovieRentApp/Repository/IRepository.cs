using MovieRentApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentApp.Repository;

public interface IRepository
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task RemoveMovie(int movieId);
}