using apiserasa.domain.dtos.pet;

namespace apiserasa.results;

public class PetResult
{
    public bool Success { get; set; }
    public List<string?>? Errors;
    public List<PetDTOResponse> PetDTOResponse { get; set; } = default!;
}