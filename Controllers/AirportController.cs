using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_2252.Controllers
{
    public class AirportController : Controller
    {
        private readonly FinalDb2252Context _dbContext;
        public AirportController(FinalDb2252Context dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: AirportController
        public async Task<ActionResult> Index()
        {
            var airport = from a in _dbContext.Airports select a;
            return View(await airport.ToListAsync());
        }

        // GET: AirportController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var airport = await _dbContext.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // GET: AirportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AirportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Airport airport)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Airports.Add(airport);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception)
                {
                    throw;
                }
            }
            return View(airport);
        }

        // GET: AirportController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var airport = await _dbContext.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // POST: AirportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Airport airport)
        {
            if (id != airport.AirportId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Airports.Update(airport);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!AirportExists(airport.AirportId))
                    {
                        return NotFound();
                    } else
                    {
                        throw;
                    }
                }
            }
            return View(airport);
            
        }

        private bool AirportExists(int id)
        {
            return _dbContext.Airports.Any(a => a.AirportId == id);
        }

        // GET: AirportController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var airport = await _dbContext.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // POST: AirportController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var airport = await _dbContext.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            _dbContext.Airports.Remove(airport);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
