namespace apiserasa.infra.entities;

public class Pet
{
    public Pet(string name, List<string?> vacinas)
    {
        Name = name;
        Vacinas = vacinas;
    }
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public List<string?> Vacinas { get; private set; } = default!;
    public byte Age { get; private set; } = default!;
}
