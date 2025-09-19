using System.ComponentModel.DataAnnotations;

namespace apiserasa.routes.requests.create;

public class UserRequestCreate
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;
}
