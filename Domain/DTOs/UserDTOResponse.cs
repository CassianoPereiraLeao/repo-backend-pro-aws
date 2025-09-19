using apiserasa.ownedtypes;

namespace apiserasa.domain.dtos;

public class UserDTOResponse
{
    public UserDTOResponse(Guid id, string name, Email email, string profile)
    {
        Id = id;
        Name = name;
        Email = email;
        Profile = profile;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Profile { get; private set; }
}