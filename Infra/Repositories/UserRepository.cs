using apiserasa.domain.dtos;
using apiserasa.infra.data;
using apiserasa.infra.entities;
using apiserasa.infra.interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiserasa.infra.repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<UserDTOResponse>> GetAllUsers(int page, int limit)
    {
        var usersQuery = _context.Users
        .OrderBy(u => u.Name)
        .Skip((page - 1) * limit)
        .Take(limit)
        .AsNoTracking()
        .Select(u => new UserDTOResponse(
            u.Id,
            u.Name,
            u.Email,
            u.Profile
        ));

        return await usersQuery.ToListAsync();
    }

    public async Task<UserDTOResponse?> GetUserById(Guid id)
    {
        var user = _context.Users.AsQueryable()
        .Where(u => u.Id == id)
        .AsNoTracking()
        .Select(u => new UserDTOResponse(
            u.Id,
            u.Name,
            u.Email,
            u.Profile
        ));

        return await user.FirstOrDefaultAsync();
    }

    public async Task<User?> Login(UserDTOLogin login)
    {
        var user = _context.Users.AsQueryable();
        user = user.Where(u => u.Email == login.Email);

        if (user == null)
            return null;

        return await user.FirstOrDefaultAsync();
    }

    public async Task<User?> UpdateUser(Guid id, UserDTOUpdate user)
    {
        var userFind = await _context.Users.FindAsync(id);

        if (userFind == null)
            return null;

        if (!string.IsNullOrEmpty(user.Name))
            userFind.UpdateName(user.Name);
        

        if (user.Email != null)
            userFind.UpdateEmail(user.Email);
        

        if (user.Password != null)
            userFind.UpdatePassword(user.Password);

        _context.Users.Update(userFind);
        await _context.SaveChangesAsync();

        return userFind;
    }
}
