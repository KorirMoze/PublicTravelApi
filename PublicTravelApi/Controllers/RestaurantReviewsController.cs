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
    [Route("/api/RestaurantReviews")]
    public class RestaurantReviewsController : Controller
    {
        private readonly DataContext _context;

        public RestaurantReviewsController(DataContext context)
        {
            _context = context;
        }

        // GET: RestaurantReviews
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.restrauntReviews.Include(r => r.restraunt);
            return View(await dataContext.ToListAsync());
        }

        // GET: RestaurantReviews/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.restrauntReviews == null)
            {
                return NotFound();
            }

            var restaurantReview = await _context.restrauntReviews
                .Include(r => r.restraunt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantReview == null)
            {
                return NotFound();
            }

            return View(restaurantReview);
        }

        // GET: RestaurantReviews/Create
        public IActionResult Create()
        {
            ViewData["RestrauntId"] = new SelectList(_context.restraunts, "Id", "Id");
            return View();
        }

        // POST: RestaurantReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Email,Body,RestrauntId,createdAt,UpdatedAt")] RestaurantReview restaurantReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurantReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestrauntId"] = new SelectList(_context.restraunts, "Id", "Id", restaurantReview.RestrauntId);
            return View(restaurantReview);
        }

        // GET: RestaurantReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.restrauntReviews == null)
            {
                return NotFound();
            }

            var restaurantReview = await _context.restrauntReviews.FindAsync(id);
            if (restaurantReview == null)
            {
                return NotFound();
            }
            ViewData["RestrauntId"] = new SelectList(_context.restraunts, "Id", "Id", restaurantReview.RestrauntId);
            return View(restaurantReview);
        }

        // POST: RestaurantReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,Email,Body,RestrauntId,createdAt,UpdatedAt")] RestaurantReview restaurantReview)
        {
            if (id != restaurantReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantReviewExists(restaurantReview.Id))
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
            ViewData["RestrauntId"] = new SelectList(_context.restraunts, "Id", "Id", restaurantReview.RestrauntId);
            return View(restaurantReview);
        }

        // GET: RestaurantReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.restrauntReviews == null)
            {
                return NotFound();
            }

            var restaurantReview = await _context.restrauntReviews
                .Include(r => r.restraunt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantReview == null)
            {
                return NotFound();
            }

            return View(restaurantReview);
        }

        // POST: RestaurantReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.restrauntReviews == null)
            {
                return Problem("Entity set 'DataContext.restrauntReviews'  is null.");
            }
            var restaurantReview = await _context.restrauntReviews.FindAsync(id);
            if (restaurantReview != null)
            {
                _context.restrauntReviews.Remove(restaurantReview);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantReviewExists(int id)
        {
          return (_context.restrauntReviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
