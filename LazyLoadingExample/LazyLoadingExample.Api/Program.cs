using LazyLoadingExample.Api.Models;
using Microsoft.EntityFrameworkCore;
using static Bogus.DataSets.Name;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o=>o.UseLazyLoadingProxies()
.UseSqlite("Data Source = mydb.db"));

builder.Services.ConfigureHttpJsonOptions(options => { options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; });

var app = builder.Build();

app.MapGet("/", async (AppDbContext context) => {
    //foreach (var genre in await context.Genres.ToListAsync())
    //{
    //    foreach (var book in genre.Books)
    //    {
    //        Console.WriteLine($"===> genre: {genre.Name}, Book: {book.Title}, Price : {book.Author}");
    //    }
    //}

    var genres = await context.Genres.Include(g => g.Books).ToListAsync();
    foreach (var genre in genres)
    {
        foreach (var book in genre.Books) {
            Console.WriteLine($"===> genre: {genre.Name}, Book: {book.Title}, Price : {book.Author}");
        }
    }

    return Results.Ok("Ok");
});

//if (app.Environment.IsDevelopment())
//{
//    using var scope = app.Services.CreateScope();
//    await SeedData(scope);
//}

app.Run();

static List<Genre> GetGenres()
{
    return new List<Genre>
    {
        new Genre
        {
            Name = "Science Fiction",
            Books = new List<Book>
            {
                new Book { Title = "Echoes of the Stars", Author = "Lena Hart" },
                new Book { Title = "The Quantum Horizon", Author = "Miles Kettering" },
                new Book { Title = "Voyagers of Andromeda", Author = "Cynthia Rowe" },
                new Book { Title = "Neon Future", Author = "David Kalmar" },
                new Book { Title = "Orbit of Silence", Author = "R.J. Maddox" },
                new Book { Title = "Shadows on Europa", Author = "Mark Feldman" },
            }
        },
        new Genre
        {
            Name = "Fantasy",
            Books = new List<Book>
            {
                new Book { Title = "The Silver Grove", Author = "Alana Rivers" },
                new Book { Title = "Crown of Mist", Author = "Tobias Wynne" },
                new Book { Title = "Dragonfire Oath", Author = "Kara Blayde" },
                new Book { Title = "The Shattered Kingdom", Author = "Evan Marlowe" },
                new Book { Title = "Runes of the Ancients", Author = "Gwen Halberg" },
                new Book { Title = "A Song for the Phoenix", Author = "Michaela Thorn" },
                new Book { Title = "The Last Enchanter", Author = "R. L. Bramson" },
            }
        },
        new Genre
        {
            Name = "Horror",
            Books = new List<Book>
            {
                new Book { Title = "The Hollow Night", Author = "Samuel Pike" },
                new Book { Title = "Ashwood Asylum", Author = "Marjorie Keene" },
                new Book { Title = "Beneath the Black Lake", Author = "Daniel Kress" },
                new Book { Title = "Creepstone Manor", Author = "Olivia Mercer" },
                new Book { Title = "The Whispering Room", Author = "Gareth Lowell" }
            }
        },
        new Genre
        {
            Name = "Mystery",
            Books = new List<Book>
            {
                new Book { Title = "The Vanishing Key", Author = "Isabelle Crane" },
                new Book { Title = "Murder at Redhall Station", Author = "Colin Ferris" },
                new Book { Title = "Secrets of Willow Lane", Author = "Julia Harwood" },
                new Book { Title = "The Case of the Glass Dagger", Author = "Victor Allard" },
                new Book { Title = "Silent Witnesses", Author = "Dana Whitford" },
                new Book { Title = "Death in the Ivy House", Author = "Helen Corwin" }
            }
        },
        new Genre
        {
            Name = "Historical Fiction",
            Books = new List<Book>
            {
                new Book { Title = "The Emperor’s Messenger", Author = "Stephen Lorne" },
                new Book { Title = "A Winter in Vienna", Author = "Clara Voigt" },
                new Book { Title = "Swords of the Republic", Author = "Marcus Hawthorne" },
                new Book { Title = "The Silk Merchant’s Daughter", Author = "Evelyn Marin" },
                new Book { Title = "Under the Iron Banner", Author = "Graham Kessler" },
                new Book { Title = "The Paris Letters", Author = "Marion Devereaux" }
            }
        }
    };
}

static async Task SeedData(IServiceScope scope)
{
    var context = scope.ServiceProvider.GetService<AppDbContext>() ?? throw new InvalidOperationException("DbContext is null");

    if (context.Database.GetPendingMigrations().Any())
    {
        await context.Database.MigrateAsync();
    }

    if (!context.Genres.Any() && !context.Books.Any())
    {
        if (!context.Genres.Any() && !context.Books.Any())
        {
            List<Genre> genres = GetGenres();
            context.Genres.AddRange(genres);
            context.SaveChanges();
        }
    }
}