using System;

namespace MovieRentApp.MVVM.Models;

public class Rental
{
    public int MovieId { get; set; }
    public int ClientId { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public virtual Client Client { get; set; }
    public virtual Movie Movie { get; set; }

    public object Clone()
    {
        return new Rental
        {
            MovieId = MovieId,
            ClientId = ClientId,
            ReturnDate = ReturnDate,
            RentDate = DateTime.Now,

        };
    }
}
