using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using day6_Movie.Models;

public class MovieController : Controller
{
    private readonly day6_MovieContext _context;

    public MovieController(day6_MovieContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var movies = _context.Movie;
        return View(await movies.ToListAsync());
    }

    // GET: MOVIES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        return View(movie);
    }

    // GET: MOVIES/Create 
    public IActionResult Create()
    {
        ViewData["GunreId"] = new SelectList(_context.Gunre, "Id", "Name");
        return View();
    }

    // POST: MOVIES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Price,GunreId")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["GunreId"] = new SelectList(_context.Gunre, "Id", "Name", movie.GunreId);
        return View(movie);
    }

    // GET: MOVIES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null) return NotFound();

        ViewData["GunreId"] = new SelectList(_context.Gunre, "Id", "Name", movie.GunreId);
        return View(movie);
    }

    // POST: MOVIES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,Price,GunreId")] Movie movie)
    {
        if (id != movie.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["GunreId"] = new SelectList(_context.Gunre, "Id", "Name", movie.GunreId);
        return View(movie);
    }

    // GET: MOVIES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        return View(movie);
    }

    // POST: MOVIES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movie.FindAsync(id);
        if (movie != null)
        {
            _context.Movie.Remove(movie);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int? id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}