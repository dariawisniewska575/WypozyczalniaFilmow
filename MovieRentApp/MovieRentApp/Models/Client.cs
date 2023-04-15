using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRentApp.Models;

public class Client
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }

    public virtual IEnumerable<Rental> RentMovies { get; set; }
}
