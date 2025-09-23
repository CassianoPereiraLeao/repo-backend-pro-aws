using apiserasa.domain.dtos.pet;
using apiserasa.infra.data;
using apiserasa.infra.entities;
using apiserasa.infra.interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiserasa.infra.repositories;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Pet> CreatePet(Pet pet)
    {
        await _context.Pets.AddAsync(pet);
        await _context.SaveChangesAsync();

        return pet;
    }

    public async Task<bool> DeletePet(Guid id)
    {
        var pet = await _context.Pets.FindAsync(id);

        if (pet == null)
            return false;

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<PetDTOResponse>> GetAllPets(int page, int limit)
    {
        var pets = _context.Pets.AsQueryable()
        .OrderBy(p => p.Name)
        .Skip((page - 1) * limit)
        .Take(limit)
        .AsNoTracking()
        .Select(p => new PetDTOResponse(
            p.Id,
            p.Name,
            p.Type,
            p.Vaccines,
            p.Age,
            p.AnimalSize,
            p.Locale
        ));

        return await pets.ToListAsync();
    }

    public async Task<Pet?> GetPetById(Guid id)
    {
        var pet = _context.Pets.AsQueryable()
        .Where(p => p.Id == id);

        return await pet.FirstOrDefaultAsync();
    }

    public async Task<Pet?> UpdatePet(Guid id, PetDTOUpdate petDTO)
    {
        var pet = await _context.Pets.FindAsync(id);

        if (pet == null)
            return pet;

        if (!string.IsNullOrEmpty(petDTO.Name))
            pet.UpdateName(petDTO.Name);

        if (!string.IsNullOrEmpty(petDTO.Type))
            pet.UpdateType(petDTO.Type);

        if (!string.IsNullOrEmpty(petDTO.Vaccines))
            pet.UpdateVaccines(petDTO.Vaccines);

        if (petDTO.Age != null)
            pet.UpdateAge((byte)petDTO.Age);

        if (!string.IsNullOrEmpty(petDTO.AnimalSize))
            pet.UpdateAnimalSize(petDTO.AnimalSize);

        if (!string.IsNullOrEmpty(petDTO.Locale))
            pet.UpdateLocale(petDTO.Locale);

        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();

        return pet;
    }
}