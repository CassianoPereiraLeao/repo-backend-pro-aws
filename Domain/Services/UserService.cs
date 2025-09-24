using apiserasa.domain.dtos.user;
using apiserasa.domain.interfaces;
using apiserasa.infra.entities;
using apiserasa.infra.interfaces;
using apiserasa.ownedtypes;
using apiserasa.results;

namespace apiserasa.domain.services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserResult> CreateUser(UserDTO userDTO)
    {
        List<string?> errors = [];
        if (!userDTO.Email.IsValid())
            errors.Add(userDTO.Email.GetError());
        

        if (!userDTO.Password.IsValid())
            errors.Add(userDTO.Password.GetError());
        

        if (errors.Count > 0)
        {
            return new UserResult
            {
                Success = false,
                Errors = errors
            };
        }

        var user = new User(
            userDTO.Name,
            userDTO.Email,
            new Password(userDTO.Password.ToHash()),
            "User"
        );

        var response = await _repository.CreateUser(user);
        if (response == null)
        {
            return new UserResult
            {
                Success = false,
                Errors = ["Usuário já existente no banco de dados"]
            };
        }

        return new UserResult
        {
            Success = true,
            Errors = null,
            UsersDTOResponse = [new UserDTOResponse(response.Id, response.Name, response.Email, response.Profile, response.PetId)]
        };
    }

    public async Task<UserResult> DeleteUser(Guid id)
    {
        bool removed = await _repository.DeleteUser(id);

        if (removed)
        {
            return new UserResult
            {
                Success = false,
                Errors = ["Usuário não encontrado"]
            };
        }

        return new UserResult
        {
            Success = true
        };
    }

    public async Task<UserResult> GetAllUsers(int? page)
    {
        int currentPage = page ?? 1;
        const int limit = 30;

        var users = await _repository.GetAllUsers(currentPage, limit);

        if (users == null)
        {
            return new UserResult
            {
                Success = false,
                Errors = ["Houve um erro pegar os usuários"]
            };
        }

        return new UserResult
        {
            Success = true,
            Errors = null,
            UsersDTOResponse = users
        };
    }

    public async Task<UserResult> GetUserById(Guid id)
    {
        var user = await _repository.GetUserById(id);

        if (user == null)
        {
            return new UserResult
            {
                Success = false,
                Errors = ["Usuário não encontrado"]
            };
        }

        return new UserResult
        {
            Success = true,
            Errors = null,
            UsersDTOResponse = [user]
        };
    }

    public async Task<UserResult> Login(UserDTOLogin userDTO)
    {
        List<string?> errors = [];
        var login = await _repository.Login(userDTO);
        if (!userDTO.Email.IsValid())
            errors.Add(userDTO.Email.GetError());

        if (!userDTO.Password.IsValid())
            errors.Add(userDTO.Password.GetError());

        if (login == null || !login.Password.ValidateRequest(userDTO.Password.ToString()))
            errors.Add("Email ou senha do usuário incorretos");

        if (errors.Count > 0 || login == null)
        {
            return new UserResult
            {
                Success = false,
                Errors = errors
            };
        }

        return new UserResult
        {
            Success = true,
            Errors = null,
            UsersDTOResponse = [new UserDTOResponse(login.Id, login.Name, login.Email, login.Profile, login.PetId)]
        };
    }

    public async Task<UserResult> UpdateUser(Guid id, UserDTOUpdate userDTO)
    {
        List<string?> errors = [];
        if (userDTO.Email != null && !userDTO.Email.IsValid())
            errors.Add(userDTO.Email.GetError());
        

        if (userDTO.Password != null && !userDTO.Password.IsValid())
            errors.Add(userDTO.Password.GetError());
        
        if (errors.Count > 0)
            {
                return new UserResult
                {
                    Success = false,
                    Errors = errors
                };
            }

        var user = await _repository.UpdateUser(id, userDTO);

        if (user == null)
        {
            return new UserResult
            {
                Success = false,
                Errors = ["Usuário não encontrado"]
            };
        }

        return new UserResult
        {
            Success = true,
            Errors = null,
            UsersDTOResponse = [new UserDTOResponse(user.Id, user.Name, user.Email, user.Profile, user.PetId)]
        };
    }
}
