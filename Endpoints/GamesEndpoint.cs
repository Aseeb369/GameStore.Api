// GameStore.Api.Endpoints/GamesEndpoint.cs
using GameStore.Api.Dtos;
using MinimalApis.Extensions;

public static class GamesEndpoint
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<Gamedto> games = new()
    {
        new Gamedto(1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new Gamedto(2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
        new Gamedto(3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))
    };

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                       .WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(g => g.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            var game = new Gamedto(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        })
        .WithParameterValidation();

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);
            if (index == -1) return Results.NotFound();

            games[index] = new Gamedto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        })
        .WithParameterValidation();

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);
            return Results.NoContent();
        });

        return group;
    }
}
