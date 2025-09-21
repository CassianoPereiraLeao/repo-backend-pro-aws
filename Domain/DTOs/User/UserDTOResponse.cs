using apiserasa.ownedtypes;

namespace apiserasa.domain.dtos.user;

public class UserDTOResponse
{
    public UserDTOResponse(Guid id, string name, Email email, string profile, Guid petId)
    {
        Id = id;
        Name = name;
        Email = email;
        Profile = profile;
        PetId = petId;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Profile { get; private set; }
    public Guid PetId { get; private set; }
}