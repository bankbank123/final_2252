using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Final_2252.Controllers
{
    public class FlightController : Controller
    {
        private readonly FinalDb2252Context _dbcontext;
        public FlightController(FinalDb2252Context dbcontext)
        {
            _dbcontext = dbcontext;
        }
        // GET: FightController
        public async Task<ActionResult> Index()
        {
            var flights = _dbcontext.Flights
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

            var flight = await _dbcontext.Flights
                .Include(f => f.AirportSourceNavigation)
                .Include(f => f.AirportDestinationNavigation)
                .FirstOrDefaultAsync(f => f.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
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
        public async Task<ActionResult> Create([Bind("FlightId ,FlightNo, AirportSource, AirportDestination, FirstName, MiddleName, LastName, DepartDate, BoardingTime, Gate, Zone, Seat, Seq")] Flight fight,
            Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            
            
            if (!modelState.IsValid)
            {
                ViewData["AirportID"] = new SelectList(_dbcontext.Airports, "AirportId", "AirportName", "AirportCode", "Address");
                return View(fight);
            }

            if (fight.AirportDestination == fight.AirportSource)
            {
                ViewData["AirportID"] = new SelectList(_dbcontext.Airports, "AirportId", "AirportName", "AirportCode", "Address");
                ModelState.AddModelError("", "Airport of origin and destination cannot be the same.");
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
            var airport = await _dbcontext.Flights.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // POST: FightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("FlightId,FlightNo, AirportSource, AirportDestination, FirstName, MiddleName, LastName, DepartDate, BoardingTime, Gate, Zone, Seat, Seq")] Flight fight,
            Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {

            if (fight.AirportDestination == fight.AirportSource)
            {
                ModelState.AddModelError("", "Airport of origin and destination cannot be the same.");
                return View(); 
            }

            if (id != fight.FlightId)
            {
                return NotFound();
            }

            if (modelState.IsValid)
            {

                try
                {
                    _dbcontext.Flights.Update(fight);
                    await _dbcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FightExists(fight.FlightId))
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
            return _dbcontext.Flights.Any(a => a.FlightId == id);
        }
        // GET: FightController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var flight = await _dbcontext.Flights
                .Include(f => f.AirportSourceNavigation)
                .Include(f => f.AirportDestinationNavigation)
                .FirstOrDefaultAsync(f => f.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: FightController/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var fight = await _dbcontext.Flights.FindAsync(id);
            if (fight == null)
            {
                return NotFound();
            }
            _dbcontext.Flights.Remove(fight);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Search(string q)
        {
            var flight = await _dbcontext.Flights
                .Include(f => f.AirportSourceNavigation)
                .Include(f => f.AirportDestinationNavigation)
                .FirstOrDefaultAsync(f => f.FlightNo == q);

            if (flight == null)
            {
                return NotFound(); 
            }

            return RedirectToAction("Details", new { id = flight.FlightId });
        }
    }
}
