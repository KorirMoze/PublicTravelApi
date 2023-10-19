using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PublicTravelApi.Models;
using TravelAPI.DataAccessLayer;

namespace PublicTravelApi.Controllers
{
    [ApiController]
    [Route("api/Restaurant")]
    public class RestaurantsController : Controller
    {
        private readonly DataContext _context;

        public RestaurantsController(DataContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Restaurants = await _context.restraunts.ToListAsync();
            if (Restaurants != null)
            {
                return Ok(Restaurants);
            }
            else
            {
                return BadRequest("Restaurants are not available");
            }
             
        }

        // GET: Restaurants/Details/5
        [HttpGet("{Id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.restraunts == null)
            {
                return NotFound();
            }

            var restaurant = await _context.restraunts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        // Post: Restaurants/Create
     //

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,Description,Keywords,PhoneNumber,RestrauntEmail,Opens,Closes,City,State,PostalCode,Phone,Price,ReserveUrl,ImageUrl,ReservePhoneNumberUrl,createdAt,UpdatedAt")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        [HttpPost("{Id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.restraunts == null)
            {
                return NotFound();
            }

            var restaurant = await _context.restraunts.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Restaurants/Delete/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.restraunts == null)
            {
                return NotFound();
            }

            var restaurant = await _context.restraunts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.restraunts == null)
            {
                return Problem("Entity set 'DataContext.restraunts'  is null.");
            }
            var restaurant = await _context.restraunts.FindAsync(id);
            if (restaurant != null)
            {
                _context.restraunts.Remove(restaurant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
          return (_context.restraunts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
