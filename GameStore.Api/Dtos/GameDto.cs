namespace GameStore.Api.Dtos;

public record class Gamedto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);


