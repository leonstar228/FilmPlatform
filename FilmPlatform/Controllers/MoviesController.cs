using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;

namespace MovieLibrary.Controllers;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public MoviesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index(string? search, MediaType? type)
    {
        var query = _context.Movies.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(m => m.Title.Contains(search));
        }
        
        if (type.HasValue)
        {
            query = query.Where(m => m.Type == type.Value);
        }
        
        var movies = await query.OrderByDescending(m => m.Rating).ToListAsync();
        
        ViewData["Search"] = search;
        ViewData["Type"] = type;
        
        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = _userManager.GetUserId(User);
            var userMovieIds = await _context.UserMovies
                .Where(um => um.UserId == userId)
                .Select(um => um.MovieId)
                .ToListAsync();
            ViewData["UserMovieIds"] = userMovieIds;
        }
        
        return View(movies);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return NotFound();
        
        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = _userManager.GetUserId(User);
            var userMovie = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == id);
            ViewData["UserMovie"] = userMovie;
        }
        
        return View(movie);
    }
}