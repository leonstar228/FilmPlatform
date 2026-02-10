using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

        return View(movies);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Movie movie)
    {
        if (!ModelState.IsValid)
            return View(movie);

        movie.CreatedAt = DateTime.UtcNow;

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Movie movie)
    {
        if (id != movie.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(movie);

        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
