using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;

namespace MovieLibrary.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var topMovies = await _context.Movies
            .OrderByDescending(m => m.Rating ?? 0)
            .Take(6)
            .ToListAsync();

        return View(topMovies);
    }
}