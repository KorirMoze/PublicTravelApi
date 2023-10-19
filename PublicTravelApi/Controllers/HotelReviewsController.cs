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
    public class HotelReviewsController : Controller
    {
        private readonly DataContext _context;

        public HotelReviewsController(DataContext context)
        {
            _context = context;
        }

        // GET: HotelReviews
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.hotelReviews.Include(h => h.hotel);
            return View(await dataContext.ToListAsync());
        }

        // GET: HotelReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.hotelReviews == null)
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

            return View(hotelReview);
        }

        // GET: HotelReviews/Create
        public IActionResult Create()
        {
            ViewData["HotelId"] = new SelectList(_context.hotels, "Id", "Id");
            return View();
        }

        // POST: HotelReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Email,Body,HotelId,createdAt,UpdatedAt")] HotelReview hotelReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.hotels, "Id", "Id", hotelReview.HotelId);
            return View(hotelReview);
        }

        // GET: HotelReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.hotelReviews == null)
            {
                return NotFound();
            }

            var hotelReview = await _context.hotelReviews.FindAsync(id);
            if (hotelReview == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.hotels, "Id", "Id", hotelReview.HotelId);
            return View(hotelReview);
        }

        // POST: HotelReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,Email,Body,HotelId,createdAt,UpdatedAt")] HotelReview hotelReview)
        {
            if (id != hotelReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelReviewExists(hotelReview.Id))
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
            ViewData["HotelId"] = new SelectList(_context.hotels, "Id", "Id", hotelReview.HotelId);
            return View(hotelReview);
        }

        // GET: HotelReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.hotelReviews == null)
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

            return View(hotelReview);
        }

        // POST: HotelReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.hotelReviews == null)
            {
                return Problem("Entity set 'DataContext.hotelReviews'  is null.");
            }
            var hotelReview = await _context.hotelReviews.FindAsync(id);
            if (hotelReview != null)
            {
                _context.hotelReviews.Remove(hotelReview);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelReviewExists(int id)
        {
          return (_context.hotelReviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
