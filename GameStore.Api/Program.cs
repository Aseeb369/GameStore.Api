using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameEndpointName = "GetGame";
List<Gamedto> games = [
    new(
    1,
    "Street Figther II",
    "Fighting",
    19.99M,
    new DateOnly(1992,7,15)),
    new(
        2,
        "Fnal  Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)),
        new (
            3,
            "FIFA 23",
            "Sports",
            69.99M,
            new DateOnly(2022,9,27) )


];
// get / games
app.MapGet("games", () => games);
//GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetGameEndpointName);

// POST / games
app.MapPost("games", (CreateGameDto newGame) =>
{
    Gamedto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);   
});

app.Run();
