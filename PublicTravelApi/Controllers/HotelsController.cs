using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAPI.DataAccessLayer;
using TravelAPI.Models;

namespace PublicTravelApi.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelsController : ControllerBase
    {
        private readonly DataContext _context;

        public HotelsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hotels = await _context.hotels.Include(x=>x.hotelReviews).ToListAsync();

            if (hotels != null)
            {
                return Ok(hotels); // Return a 200 OK response with the JSON data
            }
            else
            {
                return NotFound("No hotels found"); // Return a 404 Not Found response
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotels.FirstOrDefaultAsync(m => m.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel); // Return the hotel details as JSON
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = hotel.Id }, hotel);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest("Invalid request");
            }

            if (ModelState.IsValid)
            {
                _context.Update(hotel);
                await _context.SaveChangesAsync();
                return Ok(hotel); // Return the updated hotel as JSON
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _context.hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _context.hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return NoContent(); // Return a 204 No Content response
        }
    }


}
