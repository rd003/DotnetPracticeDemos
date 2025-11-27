namespace LazyLoadingExample.Api.Models;

public class Genre
{
    public int GenreId { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = [];
}
