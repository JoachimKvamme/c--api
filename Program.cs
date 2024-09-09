using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Registrerer en liste med Dependency Injection Container, hva nå enn det er.
        builder.Services.AddSingleton<IMovieService, MovieService()>;
        var app = builder.Build();


        // READ: Get all movies
        app.MapGet("/movies", (ImovieService movieService) =>{
            return movieService.GetAllMovies();
        });
        //CREATE: Adds a new movie
        app.MapPost("/movies", (Movie? movie, IMovieService movieService) => {
            if (movie == null) {
                return Results.BadRequest();
            }
            var createdMovie = movieService.createdMovie(movie);
            
            return Results.Created($"/movie/{createdMovie.Id}", createdMovie);

        });

        // UPDATE: Updates a movie with id
        app.MapPut("/movies/{Id}", (int Id) => $"Updates movie with id: {Id}");
        //DELETE: Deletes a movie with id
        app.MapDelete("/movies/{Id}", (int Id, List<Movie> movies) => {

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

