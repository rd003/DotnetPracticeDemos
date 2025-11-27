namespace LazyLoadingExample.Api.Models;

public class Book
{
    public int BookId { get; set; }
    public int GenreId { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
