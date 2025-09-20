using apiserasa.domain.dtos;
using apiserasa.infra.entities;

namespace apiserasa.infra.interfaces;

public interface IUserRepository
{
    Task<List<UserDTOResponse>> GetAllUsers(int page, int limit);
    Task<UserDTOResponse?> GetUserById(Guid id);
    Task<User> CreateUser(User user);
    Task<User?> Login(UserDTOLogin login);
    Task<User?> UpdateUser(Guid id, UserDTOUpdate user);
    Task<bool> DeleteUser(Guid id);
}
