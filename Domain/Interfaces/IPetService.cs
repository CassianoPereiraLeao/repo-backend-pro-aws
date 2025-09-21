using apiserasa.domain.dtos.pet;
using apiserasa.results;

namespace apiserasa.domain.interfaces;

public interface IPetService
{
    Task<PetResult> GetAllPets(int? page);
    Task<PetResult> GetPetById(Guid id);
    Task<PetResult> CreatePet(PetDTO petDTO);
    Task<PetResult> UpdatePet(Guid id, PetDTOUpdate petDTO);
    Task<PetResult> DeletePet(Guid id);
}