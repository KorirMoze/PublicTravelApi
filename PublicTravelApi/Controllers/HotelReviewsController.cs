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
    [Route("api/hotelReviews")]
    public class HotelReviewsController : ControllerBase
    {
        private readonly DataContext _context;

        public HotelReviewsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/hotelReviews
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hotelReviews = await _context.hotelReviews.ToListAsync();

            if (hotelReviews != null)
            {
                return Ok(hotelReviews);
            }
            else
            {
                return NotFound("Hotel Reviews Not Found");
            }
        }

        // GET: api/hotelReviews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelReview = await _context.hotelReviews
                .Include(h => h.hotel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hotelReview == null)
            {
                return NotFound();
            }

            return Ok(hotelReview);
        }

        // GET: api/hotelReviews/create
        // POST: api/hotelReviews
    
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HotelReview hotelReview)
        {
            if (hotelReview == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                // Implement the creation logic here (e.g., validation and saving to the database)
                _context.hotelReviews.Add(hotelReview);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Details", new { id = hotelReview.Id }, hotelReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the HotelReview.");
            }
        }


        // PUT: api/hotelReviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelReview = await _context.hotelReviews.FindAsync(id);

            if (hotelReview == null)
            {
                return NotFound();
            }

            return Ok(hotelReview);
        }

        // DELETE: api/hotelReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelReview = await _context.hotelReviews
                .Include(h => h.hotel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hotelReview == null)
            {
                return NotFound();
            }

            return Ok(hotelReview);
        }

        private bool HotelReviewExists(int id)
        {
            return (_context.hotelReviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

}
