using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRentApp.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsAvaiable { get; set; }
    public Category Category { get; set; }

    public virtual IEnumerable<Rental> Clients { get; set; }
}
