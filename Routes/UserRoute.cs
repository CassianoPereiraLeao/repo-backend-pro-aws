using apiserasa.domain.dtos;
using apiserasa.domain.interfaces;
using apiserasa.ownedtypes;
using apiserasa.routes.requests.create;
using apiserasa.routes.requests.update;
using Microsoft.AspNetCore.Mvc;

namespace apiserasa.routes;

public static class UserRoute
{
    public static void UserRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/api/user");

        route.MapGet("/", async ([FromQuery] int? page, IUserService userService) =>
        {
            var user = await userService.GetAllUsers(page);
            if (!user.Success)
            {
                return Results.BadRequest(new { errors = user.Errors});
            }
            return Results.Ok(new { users = user.UsersDTOResponse });
        }).WithTags("User");

        route.MapGet("/{id}", async (string id, IUserService userService) =>
        {
            var user = await userService.GetUserById(Guid.Parse(id));

            if (!user.Success)
            {
                return Results.BadRequest(new { error = user.Errors });
            }

            return Results.Ok(new { user = user.UsersDTOResponse });
        }).WithTags("User");

        route.MapPost("/create", async ([FromBody] UserRequestCreate userRequest, IUserService userService) =>
        {
            var userDTO = new UserDTO(
                userRequest.Name,
                new Email(userRequest.Email),
                new Password(userRequest.Password)
            );

            var response = await userService.CreateUser(userDTO);

            if (!response.Success)
            {
                return Results.BadRequest(new { errors = response.Errors });
            }

            return Results.Ok(new { user = response.UsersDTOResponse });
        }).WithTags("User");

        route.MapPost("/login", () =>
        {
            
        }).WithTags("User");

        route.MapPut("/{id}", async (string id, [FromBody] UserRequestUpdate userRequestUpdate, IUserService userService) =>
        {
            var userEmail = string.IsNullOrWhiteSpace(userRequestUpdate.Email) ? null : new Email(userRequestUpdate.Email);
            var userPassword = string.IsNullOrWhiteSpace(userRequestUpdate.Password) ? null : new Password(userRequestUpdate.Password);

            var user = await userService.UpdateUser(Guid.Parse(id), new UserDTOUpdate(
                userRequestUpdate.Name,
                userEmail,
                userPassword
            ));
            
            if (!user.Success)
            {
                return Results.BadRequest(new { errors = user.Errors });
            }
            return Results.Ok(new { user = user.UsersDTOResponse });
        }).WithTags("User");

        route.MapDelete("/", () =>
        {
            
        }).WithTags("User");
    }
}
