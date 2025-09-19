using apiserasa.ownedtypes;

namespace apiserasa.domain.dtos;

public class UserDTOUpdate
{
    public UserDTOUpdate(string? name, Email? email, Password? password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    public string? Name { get; private set; } = default!;
    public Email? Email { get; private set; } = default!;
    public Password? Password { get; private set; } = default!;
}