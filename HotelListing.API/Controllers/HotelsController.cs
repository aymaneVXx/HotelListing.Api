using Microsoft.AspNetCore.Mvc;
using HotelListing.API.Data;
// For more informations on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.API.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class HotelsController : ControllerBase
{
    private static List<Hotel> hotels = new List<Hotel>
    {
        new Hotel { Id = 1, Name = "Grand Plaza", Adress = "123 Main St", Rating = 4.5 },
        new Hotel { Id = 2, Name = "Ocean View", Adress = "456 Beach Rd", Rating = 4.8 }
    };
    
    // GET: api/<HotelsController>
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return Ok(hotels);
    }

    // GET api/<HotelsController>/5
    [HttpGet("{id}")]
    public ActionResult<int> Get(int id)
    {   
        var hotel = hotels.FirstOrDefault(h  => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return Ok(hotel);
    }

    // POST api/<HotelsController>
    [HttpPost]
    public ActionResult<Hotel> Post ([FromBody] Hotel newHotel)
    { 
        if (hotels.Any( h => h.Id == newHotel.Id )) {
            return BadRequest("Hotel with this Id already exists");
        }
        hotels.Add(newHotel);
        return CreatedAtAction(nameof(Get), new { Id = newHotel.Id}, newHotel);

    }

    // PUT api/<HotelsController>/5
    [HttpPut("{id}")]
    public ActionResult Put (int id, [FromBody] Hotel uppdatedHotel)
    {
        var existingHotel = hotels.FirstOrDefault(h =>h.Id == id);
        if (existingHotel == null)
        {
            return NotFound();
        }
        existingHotel.Name = uppdatedHotel.Name;
        existingHotel.Adress = uppdatedHotel.Adress;
        existingHotel.Rating = uppdatedHotel.Rating;

        return NoContent();
    }

    // DELETE api/<HotelsController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
        if (existingHotel == null)
        {
            return NotFound(new { message = "Hotel Not found" });
        }

        hotels.Remove(existingHotel);
        return NoContent();
    }   

}
