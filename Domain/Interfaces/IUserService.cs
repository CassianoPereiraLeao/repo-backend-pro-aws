using apiserasa.domain.dtos;
using apiserasa.results;

namespace apiserasa.domain.interfaces;

public interface IUserService
{
    Task<UserResult> GetAllUsers(int? page);
    Task<UserResult> GetUserById(Guid id);
    Task<UserResult> CreateUser(UserDTO userDTO);
    Task<UserResult> Login(UserDTOLogin userDTO);
    Task<UserResult> UpdateUser(Guid id, UserDTOUpdate userDTO);
    Task<UserResult> DeleteUser(Guid id);
}
