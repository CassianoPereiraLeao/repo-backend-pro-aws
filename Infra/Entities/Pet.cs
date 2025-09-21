using System.ComponentModel.DataAnnotations;

namespace apiserasa.infra.entities;

public class Pet
{
    public Pet(string name, string type, string vaccines, byte age, string animalSize, string locale)
    {
        Name = name;
        Type = type;
        Vaccines = vaccines;
        Age = age;
        AnimalSize = animalSize;
        Locale = locale;
    }

    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    [StringLength(40)]
    public string Name { get; private set; } = default!;
    [Required]
    [StringLength(20)]
    public string Type { get; private set; } = default!;
    [Required]
    [StringLength(255)]
    public string Vaccines { get; private set; } = default!;
    [Required]
    public byte Age { get; private set; } = default!;
    [Required]
    [StringLength(30)]
    public string AnimalSize { get; private set; } = default!;
    [Required]
    [StringLength(255)]
    public string Locale { get; private set; } = default!;

    public void UpdateName(string name) => Name = name;
    public void UpdateType(string type) => Type = type;
    public void UpdateVaccines(string vaccines) => Vaccines = vaccines;
    public void UpdateAge(byte age) => Age = age;
    public void UpdateAnimalSize(string animalSize) => AnimalSize = animalSize;
    public void UpdateLocale(string locale) => Locale = locale;
}
