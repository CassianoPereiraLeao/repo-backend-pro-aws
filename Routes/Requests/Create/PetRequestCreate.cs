using System.ComponentModel.DataAnnotations;

namespace apiserasa.routes.requests.create;

public class PetRequestCreate
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string Type { get; set; } = default!;
    [Required]
    public string Vaccines { get; set; } = default!;
    [Required]
    public byte Age { get; set; } = default!;
    [Required]
    public string AnimalSize { get; set; } = default!;
    [Required]
    public string Locale { get; set; } = default!;
}
