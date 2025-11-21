using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Data;

public class Country
{
    [Key]
    public int CountryId { get; set; }
    
    public required string Name { get; set; }
    public required string ShortName { get; set; }

    public IList<Hotel> Hotels { get; set; } = [];
}