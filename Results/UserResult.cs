using apiserasa.domain.dtos.user;

namespace apiserasa.results;

public class UserResult
{
    public bool Success { get; set; }
    public List<string?>? Errors;
    public List<UserDTOResponse> UsersDTOResponse { get; set; } = default!;
}