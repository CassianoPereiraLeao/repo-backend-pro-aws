namespace apiserasa.domain.dtos.pet;

public class PetDTOUpdate
{
    public PetDTOUpdate(string? name, string? type, string? vaccines, byte? age, string? animalSize, string? locale)
    {
        Name = name;
        Type = type;
        Vaccines = vaccines;
        Age = age;
        AnimalSize = animalSize;
        Locale = locale;
    }
    public string? Name { get; private set; } = default!;
    public string? Type { get; private set; } = default!;
    public string? Vaccines { get; private set; } = default!;
    public byte? Age { get; private set; } = default!;
    public string? AnimalSize { get; private set; } = default!;
    public string? Locale { get; private set; } = default!;
}