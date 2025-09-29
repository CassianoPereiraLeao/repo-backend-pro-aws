using apiserasa.ownedtypes;

namespace apiserasa.domain.dtos.user;

public class UserDTOUpdate
{
    public UserDTOUpdate(string? name, Email? email, Password? password, Guid? petId)
    {
        Name = name;
        Email = email;
        Password = password;
        PetId = petId;
    }
    public string? Name { get; private set; } = default!;
    public Email? Email { get; private set; } = default!;
    public Password? Password { get; private set; } = default!;
    public Guid? PetId { get; private set; } = default!;
}