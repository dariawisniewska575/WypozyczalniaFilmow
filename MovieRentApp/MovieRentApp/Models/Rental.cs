using System;

namespace MovieRentApp.Models;

public class Rental
{
    public int MovieId { get; set; }
    public int ClientId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public virtual Client Client { get; set; }
    public virtual Movie Movie { get; set; }
}
