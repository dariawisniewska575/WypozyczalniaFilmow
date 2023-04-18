using Microsoft.EntityFrameworkCore;
using MovieRentApp.Context;
using MovieRentApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRentApp.Repository;

public class Repository : IRepository
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Movie> AddMovieAsync(Movie newMovie)
    {
        await _dbContext.Movies.AddAsync(newMovie);
        await _dbContext.SaveChangesAsync();
        return newMovie;
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
        => await _dbContext.Movies.ToListAsync();

    public async Task RemoveMovieAsync(int movieId)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
        if (movie is null) return;
        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();
    }
}
