using HotelListing.API.Data;
using HotelListing.API.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController(HotelListingDbContext context) : ControllerBase
{

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotels()
    {
        var hotels = await context.Hotels
            .Select(h => new GetHotelsDto (h.Id, h.Name, h.Adress, h.Rating, h.CountryId )).ToListAsync();
        return Ok(hotels);
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
    {
        var hotel = await context.Hotels
            .Where(h  => h.Id == id)
            .Select(h => new GetHotelDto(h.Id, h.Name, h.Adress, h.Rating, h.Country!.Name))
            .FirstOrDefaultAsync();

        if (hotel == null)
        {
            return NotFound();
        }

        return hotel;
    }

    // PUT: api/Hotels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, UpdateHotelDto hotelDto)
    {
        if (id != hotelDto.Id)
        {
            return BadRequest();
        }

        var hotel = await context.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        hotel.Name = hotelDto.Name;
        hotel.Adress = hotelDto.Adress;
        hotel.Rating = hotelDto.Rating;
        hotel.CountryId = hotelDto.CountryId;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HotelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Hotels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
    {
        var hotel = new Hotel
        {
            Name = hotelDto.Name,
            Adress = hotelDto.Adress,
            Rating = hotelDto.Rating,
            CountryId = hotelDto.CountryId,
        };

        context.Hotels.Add(hotel);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await context.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }

        context.Hotels.Remove(hotel);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool HotelExists(int id)
    {
        return context.Hotels.Any(e => e.Id == id);
    }
}
