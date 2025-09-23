using System.ComponentModel.DataAnnotations;

namespace apiserasa.routes.requests.login;

public class UserRequestLogin
{
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;
}
