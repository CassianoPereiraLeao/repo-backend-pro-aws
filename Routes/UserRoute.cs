using apiserasa.domain.dtos.user;
using apiserasa.domain.interfaces;
using apiserasa.ownedtypes;
using apiserasa.routes.requests.create;
using apiserasa.routes.requests.login;
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
                return Results.BadRequest(new { success = user.Success, errors = user.Errors});

            return Results.Ok(new { success = user.Success, users = user.UsersDTOResponse });
        }).WithTags("User");

        route.MapGet("/{id}", async (string id, IUserService userService) =>
        {
            var user = await userService.GetUserById(Guid.Parse(id));

            if (!user.Success)
                return Results.BadRequest(new { success = user.Success, error = user.Errors });

            return Results.Ok(new { success = user.Success, user = user.UsersDTOResponse });
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
                return Results.BadRequest(new { success = response.Success, errors = response.Errors });

            return Results.Created();
        }).WithTags("User");

        route.MapPost("/login", async ([FromBody] UserRequestLogin userRequestLogin, IUserService userService) =>
        {
            var userLogin = new UserDTOLogin(
                new Email(userRequestLogin.Email),
                new Password(userRequestLogin.Password)
            );
            
            var login = await userService.Login(userLogin);

            if (!login.Success)
                return Results.BadRequest(new { success = login.Success, errors = login.Errors });

            return Results.Ok(new { success = login.Success, user = login.UsersDTOResponse });
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
                return Results.BadRequest(new { success = user.Success, errors = user.Errors });

            return Results.Ok(new { success = user.Success, user = user.UsersDTOResponse });
        }).WithTags("User");

        route.MapDelete("/{id}", async (string id, IUserService userService) =>
        {
            var user = await userService.DeleteUser(Guid.Parse(id));

            if (!user.Success)
                return Results.BadRequest(new { success = user.Success, error = user.Errors });

            return Results.Ok(new { success = user.Success });
        }).WithTags("User");
    }
}
