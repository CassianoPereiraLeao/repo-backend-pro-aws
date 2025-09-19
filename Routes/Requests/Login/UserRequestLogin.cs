namespace apiserasa.routes.requests;

public class UserRequestLogin
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
