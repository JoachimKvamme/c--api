using Microsoft.VisualBasic;

class Movie {

    //private static int _id = 0;


    // Jeg har prøvd å gjøre det sånn at Id-egenskapen konstrueres som et Guid-objekt 
    // når det lages nye tilfeller av Movie-klassen. Dette var bare for å se om det virket, 
    // slik jeg forstår det brukes Guid bare når man trenger å skape objekter med unike identiteter 
    // hvor opphavet er usikkert. Her kommer alle objektene fra meg, og metoden med den statiske variabelen
    // ville ikke ha skapt noen problemer. 
    
    public Guid Id {get; set;}
    public string Title {get; set;}

    // Lager en valgfri egenskap, her en beskrivelse av filmen i Movie-objektet.
    public string? Description {get; set;}

    public Movie(string title, string? description) {

        Description = description;
        Title = title;

        // Nå skapes det en Guid-id for hvert Movie-objekt, men det gjør at DELETE-funksjonen er noe tung å håndtere.
        Id = Guid.NewGuid();
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
        app.MapPut("/movies/{Id}", (Guid Id) => $"Updates movie with id: {Id}");
        //DELETE: Deletes a movie with id
        app.MapDelete("/movies/{Id}", (Guid Id, List<Movie> movies) => {

            var movie = movies.Find((movie) => {
                return movie.Id == Id;});
            
            if (movie == null) {
                return Results.NotFound();
            }
            movies.Remove(movie);
            return Results.Ok();

        });

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

