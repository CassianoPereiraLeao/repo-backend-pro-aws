using System.ComponentModel.DataAnnotations;
using apiserasa.ownedtypes;

namespace apiserasa.infra.entities;

public class User
{
    protected User() { }
    public User(string name, Email email, Password password, string profile)
    {
        Name = name;
        Email = email;
        Password = password;
        Profile = profile;
    }

    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Name { get; private set; } = default!;
    [Required]
    [EmailAddress]
    [MinLength(10)]
    [MaxLength(100)]
    public Email Email { get; private set; } = default!;
    [Required]
    [MinLength(8)]
    public Password Password { get; private set; } = default!;
    [Required]
    public string Profile { get; private set; } = default!;
    public Guid PetId { get; private set; } = default!;

    public void UpdateName(string name) => Name = name;
    public void UpdateEmail(Email email) => Email = email;
    public void UpdatePassword(Password password) => Password = password;
    public void UpdateProfile(string profile) => Profile = profile;
    public void UpdatePetid(Guid petid) => PetId = petid;
}
