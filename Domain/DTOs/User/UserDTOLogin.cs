using apiserasa.ownedtypes;

namespace apiserasa.domain.dtos.user;

public class UserDTOLogin
{
    public UserDTOLogin(Email email, Password password)
    {
        Email = email;
        Password = password;
    }
    public Email Email { get; private set; } = default!;
    public Password Password { get; private set; } = default!;
}