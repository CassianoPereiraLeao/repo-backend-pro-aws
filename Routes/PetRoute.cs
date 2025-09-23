using apiserasa.domain.dtos.pet;
using apiserasa.domain.interfaces;
using apiserasa.routes.requests.create;
using apiserasa.routes.requests.update;
using Microsoft.AspNetCore.Mvc;

namespace apiserasa.routes;

public static class PetRoute
{
    public static void PetRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/api/pet");

        route.MapGet("/", async ([FromQuery] int? page, IPetService petService) =>
        {
            var pets = await petService.GetAllPets(page);

            if (!pets.Success)
                return Results.BadRequest(new { success = pets.Success, errors = pets.Errors });

            return Results.Ok(new { success = pets.Success, pets = pets.PetDTOResponse });
        }).WithTags("Pet");

        route.MapGet("/{id}", async (string id, IPetService petService) =>
        {
            var pet = await petService.GetPetById(Guid.Parse(id));

            if (!pet.Success)
                return Results.NotFound(new { success = pet.Success, errors = pet.Errors });

            return Results.Ok(new { success = pet.Success, pet = pet.PetDTOResponse });
        }).WithTags("Pet");

        route.MapPost("/create", async ([FromBody] PetRequestCreate petRequest, IPetService petService) =>
        {
            var pet = new PetDTO(
                petRequest.Name,
                petRequest.Type,
                petRequest.Vaccines,
                petRequest.Age,
                petRequest.AnimalSize,
                petRequest.Locale
            );
            var create = await petService.CreatePet(pet);

            if (!create.Success)
                return Results.BadRequest(new { success = create.Success, errors = create.Errors });

            return Results.Created();
        }).WithTags("Pet");

        route.MapPut("/{id}", async (string id, [FromBody] PetRequestUpdate petRequest, IPetService petService) =>
        {
            var petDTO = new PetDTOUpdate(
                petRequest.Name ?? null,
                petRequest.Type ?? null,
                petRequest.Vaccines ?? null,
                petRequest.Age ?? null,
                petRequest.AnimalSize ?? null,
                petRequest.Locale ?? null
            );
            var pet = await petService.UpdatePet(Guid.Parse(id), petDTO);

            if (!pet.Success)
                return Results.NotFound(new { success = pet.Success, errors = pet.Errors });

            return Results.Ok(new { success = pet.Success, pet = pet.PetDTOResponse });
        }).WithTags("Pet");

        route.MapDelete("/{id}", async (string id, IPetService petService) =>
        {
            var deleted = await petService.DeletePet(Guid.Parse(id));

            if (!deleted.Success)
                return Results.NotFound(new { success = deleted.Success, errors = deleted.Errors });

            return Results.Ok(new { sucess = deleted.Success });
        }).WithTags("Pet");
    }
}
