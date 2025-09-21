using apiserasa.domain.interfaces;

namespace apiserasa.routes;

public static class PetRoute
{
    public static void PetRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/api/pet");

        route.MapGet("/{page}", (int? page, IPetService petService) =>
        {

        });
    }
}
