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
    public class HotelsController : Controller
    {
        private readonly DataContext _context;

        public HotelsController(DataContext context)
        {
            _context = context;
        }

        // GET: Hotels
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hotels = await _context.hotels.ToListAsync();

            if (hotels != null)
            {
                return Ok(hotels); // Return a 200 OK response with the JSON data
            }
            else
            {
                return NotFound("No hotels found"); // Return a 404 Not Found response
            }
        }

        // GET: Hotels/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create

      //  public IActionResult Create()
      //  {
      //      return View();
      //  }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,Description,Keywords,PhoneNumber,RestrauntEmail,Opens,Closes,City,State,PostalCode,Phone,Price,ReserveUrl,ImageUrl,ReservePhoneNumberUrl,createdAt,UpdatedAt")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,Description,Keywords,PhoneNumber,RestrauntEmail,Opens,Closes,City,State,PostalCode,Phone,Price,ReserveUrl,ImageUrl,ReservePhoneNumberUrl,createdAt,UpdatedAt")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.hotels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hotels == null)
            {
                return Problem("Entity set 'DataContext.hotels'  is null.");
            }
            var hotel = await _context.hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.hotels.Remove(hotel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
          return (_context.hotels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
