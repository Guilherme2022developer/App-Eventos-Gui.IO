using Microsoft.AspNetCore.Identity;

namespace Eventos.IO.Services.Api.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string Name { get; set; }

    [PersonalData]
    public DateTime DOB { get; set; }

    [PersonalData]
    public string CPF { get; set; }


}