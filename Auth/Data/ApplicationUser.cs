using Microsoft.AspNetCore.Identity;

namespace Auth.Data;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Cpf { get; set; } = string.Empty;
}
