class Movie {
    public string Title {get; set;}

    public Movie(string title) {
        Title = title;
    }
}


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Registrerer en liste med Dependency Injection Container, hva nå enn det er.
        builder.Services.AddSingleton<List<Movie>>();
        var app = builder.Build();


        // READ: Get all movies
        app.MapGet("/movies", (List<Movie> movies) => movies);
        //CREATE: Adds a new movie
        app.MapPost("/movies", (Movie? movie, List<Movie> movies) => {
            if (movie == null) {
                return Results.BadRequest();
            }
            movies.Add(movie);
            return Results.Created();
        });

        // UPDATE: Updates a movie with id
        app.MapPut("/movies/{Id}", (int Id) => $"Updates movie with id: {Id}");
        //DELETE: Deletes a movie with id
        app.MapDelete("/movies/{Id}", (int Id) => $"Deletes movie with id: {Id}");

        app.MapGet("/health", () => "system healthy");

        app.MapGet("/sted", () => { return 10; });
        app.Run();
    }
}

// CreateNewMovie
// ReadAllMovies
// UpdateMovieWithId
// DeleteMovieWithId

// ditt.domene /movies

/*
GET    /movies
POST   /movies
DELETE /movies/{id}
PUT    /movies/{id}
*/

