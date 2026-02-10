using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;
using MovieLibrary.ViewModels.Profile;

namespace MovieLibrary.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();
        
        var userMovies = await _context.UserMovies
            .Include(um => um.Movie)
            .Where(um => um.UserId == user.Id)
            .ToListAsync();
        
        var model = new ProfileViewModel
        {
            DisplayName = user.DisplayName ?? user.Email ?? "Користувач",
            Email = user.Email ?? "",
            AvatarUrl = user.AvatarUrl,
            RegisteredAt = user.RegisteredAt,
            TotalMovies = userMovies.Count(um => um.Movie.Type == MediaType.Movie),
            TotalSeries = userMovies.Count(um => um.Movie.Type == MediaType.Series),
            Watching = userMovies.Count(um => um.Status == WatchStatus.Watching),
            Completed = userMovies.Count(um => um.Status == WatchStatus.Completed),
            PlanToWatch = userMovies.Count(um => um.Status == WatchStatus.PlanToWatch)
        };
        
        return View(model);
    }
    
    public async Task<IActionResult> Edit()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();
        
        return View(new EditProfileViewModel
        {
            DisplayName = user.DisplayName ?? "",
            AvatarUrl = user.AvatarUrl
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditProfileViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();
        
        user.DisplayName = model.DisplayName;
        user.AvatarUrl = model.AvatarUrl;
        
        await _userManager.UpdateAsync(user);
        
        TempData["Success"] = "Профіль оновлено!";
        return RedirectToAction(nameof(Index));
    }
}