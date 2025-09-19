using apiserasa.domain.dtos;

namespace apiserasa.results;

public class UserResult
{
    public bool Success { get; set; }
    public List<string?>? Errors;
    public List<UserDTOResponse> UsersDTOResponse { get; set; } = default!;
}