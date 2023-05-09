using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRentApp.MVVM.Models;

public class Client : ICloneable
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }

    public virtual IEnumerable<Rental> RentMovies { get; set; }

    public object Clone()
    {
        return new Client
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Age = Age
        };
    }
}
