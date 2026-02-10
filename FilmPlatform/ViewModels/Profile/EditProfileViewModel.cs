using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.ViewModels.Profile;

public class EditProfileViewModel
{
    [Required(ErrorMessage = "Введіть ім'я")]
    [Display(Name = "Ім'я")]
    [StringLength(50, MinimumLength = 2)]
    public string DisplayName { get; set; } = string.Empty;
    
    [Display(Name = "URL аватара")]
    [Url(ErrorMessage = "Невірний URL")]
    public string? AvatarUrl { get; set; }
}