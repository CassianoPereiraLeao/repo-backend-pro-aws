using apiserasa.domain.dtos.pet;
using apiserasa.infra.entities;

namespace apiserasa.infra.interfaces;

public interface IPetRepository
{
    Task<List<Pet>> GetAllPets(int page, int limit);
    Task<Pet?> GetPetById(Guid id);
    Task<Pet> CreatePet(Pet petDTO);
    Task<Pet?> UpdatePet(Guid id, PetDTOUpdate userDTO);
    Task<bool> DeletePet(Guid id);
}
