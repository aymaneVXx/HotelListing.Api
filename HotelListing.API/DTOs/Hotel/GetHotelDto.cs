namespace HotelListing.API.DTOs.Hotel;

public record GetHotelDto(
    int Id,
    string Name,
    string Adress,
    double Rating,
    String Country
    );

