using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Final_2252.Controllers
{
    public class FightController : Controller
    {
        private readonly FinalDb2252Context _dbcontext;
        public FightController(FinalDb2252Context dbcontext)
        {
            _dbcontext = dbcontext;
        }
        // GET: FightController
        public async Task<ActionResult> Index()
        {
            var flights = _dbcontext.Fights
                .Include(f => f.AirportSourceNavigation)
                .Include(f => f.AirportDestinationNavigation)
                .ToListAsync();
            return View(await flights);
        }

        // GET: FightController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var fight = await _dbcontext.Fights
                .Include(f => f.AirportSourceNavigation)
                .Include(f => f.AirportDestinationNavigation)
                .FirstOrDefaultAsync(f => f.FightId == id);

            if (fight == null)
            {
                return NotFound();
            }

            return View(fight);
        }

        // GET: FightController/Create
        public ActionResult Create()
        {
            ViewData["AirportID"] = new SelectList(_dbcontext.Airports, "AirportId", "AirportName", "AirportCode", "Address");
            return View();
        }

        // POST: FightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("FightId,FightNo, AirportSource, AirportDestination, FirstName, MiddleName, LastName, DepartDate, BoardingTime, Gate, Zone, Seat, Seq")] Fight fight,
            Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                ViewData["AirportID"] = new SelectList(_dbcontext.Airports, "AirportId", "AirportName", "AirportCode", "Address");
                return View(fight);
            }


            _dbcontext.Add(fight);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: FightController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewData["AirportID"] = new SelectList(_dbcontext.Airports, "AirportId", "AirportName", "AirportCode", "Address");

            if (id == 0)
            {
                return NotFound();
            }
            var airport = await _dbcontext.Fights.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // POST: FightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("FightId,FightNo, AirportSource, AirportDestination, FirstName, MiddleName, LastName, DepartDate, BoardingTime, Gate, Zone, Seat, Seq")] Fight fight,
            Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {

            if (id != fight.FightId)
            {
                return NotFound();
            }
            if (modelState.IsValid)
            {

                try
                {
                    _dbcontext.Fights.Update(fight);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FightExists(fight.FightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(fight);

        }
        private bool FightExists(int id)
        {
            return _dbcontext.Fights.Any(a => a.FightId == id);
        }
        // GET: FightController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var fight = await _dbcontext.Fights.FindAsync(id);
            if (fight == null)
            {
                return NotFound();
            }
            return View(fight);
        }

        // POST: FightController/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var fight = await _dbcontext.Fights.FindAsync(id);
            if (fight == null)
            {
                return NotFound();
            }
            _dbcontext.Fights.Remove(fight);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Search(string q)
        {
            var flight = await _dbcontext.Fights
                .Include(f => f.AirportSourceNavigation)
                .Include(f => f.AirportDestinationNavigation)
                .FirstOrDefaultAsync(f => f.FightNo == q);

            if (flight == null)
            {
                return NotFound(); // Handle case where flight with given FightNo is not found
            }

            return RedirectToAction("Details", new { id = flight.FightId });
        }
    }
}
