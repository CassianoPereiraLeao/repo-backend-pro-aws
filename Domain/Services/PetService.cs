using apiserasa.domain.dtos.pet;
using apiserasa.domain.interfaces;
using apiserasa.infra.entities;
using apiserasa.infra.interfaces;
using apiserasa.results;

namespace apiserasa.domain.services;

public class PetService : IPetService
{
    private readonly IPetRepository _repository;
    public PetService(IPetRepository repository)
    {
        _repository = repository;
    }
    public async Task<PetResult> CreatePet(PetDTO petDTO)
    {
        var pet = new Pet(
            petDTO.Name,
            petDTO.Type,
            petDTO.Vaccines,
            petDTO.Age,
            petDTO.AnimalSize,
            petDTO.Locale
        );

        var response = await _repository.CreatePet(pet);

        if (response == null)
        {
            return new PetResult
            {
                Success = false,
                Errors = ["Erro ao criar o Pet"]
            };
        }

        return new PetResult
        {
            Success = true,
            Errors = null,
            PetDTOResponse = [new PetDTOResponse(
                response.Id,
                response.Name,
                response.Type,
                response.Vaccines,
                response.Age,
                response.AnimalSize,
                response.Locale
                )]
        };
    }

    public async Task<PetResult> DeletePet(Guid id)
    {
        var petDelete = await _repository.DeletePet(id);

        if (!petDelete)
        {
            return new PetResult
            {
                Success = false,
                Errors = ["Usuário não encontrado"]
            };
        }

        return new PetResult
        {
            Success = true,
            Errors = null
        };
    }

    public async Task<PetResult> GetAllPets(int? page)
    {
        int current_page = page ?? 1;
        int limit = 30;

        var pets = await _repository.GetAllPets(current_page, limit);

        return new PetResult
        {
            Success = true,
            Errors = null,
            PetDTOResponse = pets,
        };
    }

    public async Task<PetResult> GetPetById(Guid id)
    {
        var pet = await _repository.GetPetById(id);

        if (pet == null)
        {
            return new PetResult
            {
                Success = false,
                Errors = ["Pet não encontrado"]
            };
        }

        return new PetResult
        {
            Success = true,
            Errors = null,
            PetDTOResponse = [new PetDTOResponse(pet.Id, pet.Name, pet.Type, pet.Vaccines, pet.Age, pet.AnimalSize, pet.Locale)]
        };
    }

    public async Task<PetResult> UpdatePet(Guid id, PetDTOUpdate petDTO)
    {
        var pet = await _repository.UpdatePet(id, petDTO);

        if (pet == null)
        {
            return new PetResult
            {
                Success = false,
                Errors = ["Pet não encontrado"]
            };
        }

        return new PetResult
        {
            Success = true,
            Errors = null,
            PetDTOResponse = [new PetDTOResponse(pet.Id, pet.Name, pet.Type, pet.Vaccines, pet.Age, pet.AnimalSize, pet.Locale)]
        };
    }
}
