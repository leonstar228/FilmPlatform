using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;
using MovieLibrary.ViewModels.Library;

namespace MovieLibrary.Controllers;

[Authorize]
public class LibraryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public LibraryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index(WatchStatus? status, MediaType? type, string? search)
    {
        var userId = _userManager.GetUserId(User);
        
        var query = _context.UserMovies
            .Include(um => um.Movie)
            .Where(um => um.UserId == userId);
        
        if (status.HasValue)
        {
            query = query.Where(um => um.Status == status.Value);
        }
        
        if (type.HasValue)
        {
            query = query.Where(um => um.Movie.Type == type.Value);
        }
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(um => um.Movie.Title.Contains(search));
        }
        
        var items = await query
            .OrderByDescending(um => um.AddedAt)
            .Select(um => new UserMovieItem
            {
                UserMovieId = um.Id,
                MovieId = um.MovieId,
                Title = um.Movie.Title,
                PosterUrl = um.Movie.PosterUrl,
                Year = um.Movie.Year,
                Type = um.Movie.Type,
                Status = um.Status,
                UserRating = um.UserRating,
                AddedAt = um.AddedAt
            })
            .ToListAsync();
        
        var model = new LibraryViewModel
        {
            Items = items,
            CurrentFilter = status,
            TypeFilter = type,
            SearchQuery = search
        };
        
        return View(model);
    }
    
    public async Task<IActionResult> Add(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return NotFound();
        
        var userId = _userManager.GetUserId(User);
        
        var existing = await _context.UserMovies
            .AnyAsync(um => um.UserId == userId && um.MovieId == id);
        
        if (existing)
        {
            TempData["Warning"] = "Цей фільм вже у вашій бібліотеці";
            return RedirectToAction(nameof(Index));
        }
        
        var model = new AddToLibraryViewModel
        {
            MovieId = id,
            Movie = movie,
            Status = WatchStatus.PlanToWatch
        };
        
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddToLibraryViewModel model)
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null) return Unauthorized();
        
        var movieExists = await _context.Movies.AnyAsync(m => m.Id == model.MovieId);
        if (!movieExists)
        {
            return NotFound();
        }
        
        var existing = await _context.UserMovies
            .AnyAsync(um => um.UserId == userId && um.MovieId == model.MovieId);
        
        if (existing)
        {
            TempData["Warning"] = "Цей фільм вже у вашій бібліотеці";
            return RedirectToAction(nameof(Index));
        }
        
        var userMovie = new UserMovie
        {
            UserId = userId,
            MovieId = model.MovieId,
            Status = model.Status,
            UserRating = model.UserRating,
            Notes = model.Notes,
            WatchedAt = model.Status == WatchStatus.Completed ? DateTime.UtcNow : null,
            AddedAt = DateTime.UtcNow
        };
        
        _context.UserMovies.Add(userMovie);
        await _context.SaveChangesAsync();
        
        TempData["Success"] = "Додано до бібліотеки!";
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        
        var userMovie = await _context.UserMovies
            .Include(um => um.Movie)
            .FirstOrDefaultAsync(um => um.Id == id && um.UserId == userId);
        
        if (userMovie == null) return NotFound();
        
        var model = new AddToLibraryViewModel
        {
            MovieId = userMovie.MovieId,
            Movie = userMovie.Movie,
            Status = userMovie.Status,
            UserRating = userMovie.UserRating,
            Notes = userMovie.Notes
        };
        
        ViewData["UserMovieId"] = id;
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AddToLibraryViewModel model)
    {
        var userId = _userManager.GetUserId(User);
        
        var userMovie = await _context.UserMovies
            .FirstOrDefaultAsync(um => um.Id == id && um.UserId == userId);
        
        if (userMovie == null) return NotFound();
        
        userMovie.Status = model.Status;
        userMovie.UserRating = model.UserRating;
        userMovie.Notes = model.Notes;
        
        if (model.Status == WatchStatus.Completed && userMovie.WatchedAt == null)
        {
            userMovie.WatchedAt = DateTime.UtcNow;
        }
        
        await _context.SaveChangesAsync();
        
        TempData["Success"] = "Зміни збережено!";
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, WatchStatus status)
    {
        var userId = _userManager.GetUserId(User);
        
        var userMovie = await _context.UserMovies
            .FirstOrDefaultAsync(um => um.Id == id && um.UserId == userId);
        
        if (userMovie == null) return NotFound();
        
        userMovie.Status = status;
        
        if (status == WatchStatus.Completed && userMovie.WatchedAt == null)
        {
            userMovie.WatchedAt = DateTime.UtcNow;
        }
        
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int id)
    {
        var userId = _userManager.GetUserId(User);
        
        var userMovie = await _context.UserMovies
            .FirstOrDefaultAsync(um => um.Id == id && um.UserId == userId);
        
        if (userMovie != null)
        {
            _context.UserMovies.Remove(userMovie);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Видалено з бібліотеки";
        }
        
        return RedirectToAction(nameof(Index));
    }
}