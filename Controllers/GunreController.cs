using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using day6_Movie.Models;

public class GunreController : Controller
{
    private readonly day6_MovieContext _context;

    public GunreController(day6_MovieContext context)
    {
        _context = context;
    }

    // GET: GUNRES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Gunre.ToListAsync());
    }

    // GET: GUNRES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var gunre = await _context.Gunre
            .Include(g => g.Movies) 
            .FirstOrDefaultAsync(m => m.Id == id);

        if (gunre == null) return NotFound();

        return View(gunre);
    }

    // GET: GUNRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GUNRES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Gunre gunre)
    {
        if (ModelState.IsValid)
        {
            _context.Add(gunre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(gunre);
    }

    // GET: GUNRES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var gunre = await _context.Gunre.FindAsync(id);
        if (gunre == null) return NotFound();

        return View(gunre);
    }

    // POST: GUNRES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name")] Gunre gunre)
    {
        if (id != gunre.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(gunre);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GunreExists(gunre.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(gunre);
    }

    // GET: GUNRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var gunre = await _context.Gunre
            .FirstOrDefaultAsync(m => m.Id == id);

        if (gunre == null) return NotFound();

        return View(gunre);
    }

    // POST: GUNRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var gunre = await _context.Gunre.FindAsync(id);
        if (gunre != null)
        {
            _context.Gunre.Remove(gunre);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool GunreExists(int? id)
    {
        return _context.Gunre.Any(e => e.Id == id);
    }
}