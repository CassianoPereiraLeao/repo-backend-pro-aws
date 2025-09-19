using apiserasa.ownedtypes;
using apiserasa.results;

namespace apiserasa.domain.dtos;

public class UserDTOLogin
{
    public UserDTOLogin(Email email, Password password)
    {
        Email = email;
        Password = password;
    }

    public Email Email { get; private set; } = default!;
    public Password Password { get; private set; } = default!;

    public bool ValidEmail(Email email)
    {
        Email = email;
        // if (!email.IsValid())
        // {
        //     error.Add(email.GetError());
        //     return false;
        // }
        return true;
    }

    public bool ValidPassword(Password password)
    {
        Password = password;
        // if (!password.IsValid())
        // {
        //     UserResult.Errors.Add(password.GetError());
        //     return false;
        // }
        return true;
    }
}