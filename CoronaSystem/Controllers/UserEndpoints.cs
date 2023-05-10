using CoronaSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace CoronaSystem.Controllers;

public static class UserEndpoints
{
    public static void MapUsersEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Users").WithTags("Users");

		group.MapGet("/", () => {
			return new[] { new ResponseUser() };
		})
			.WithName("GetAllUsers")
			.WithOpenApi();


		group.MapGet("/{id:int}", (int id) => {
			//return new ResponseUser { ID = id };
		})
			.WithName("GetUserById")
			.WithOpenApi();

		group.MapGet("/view={id:int}", (int id) => {
			//return new ResponseUser { ID = id };
		})
			.WithName("ViewUserImage")
			.WithOpenApi();


		group.MapGet("/city={city:alpha}", (String city) => {
			//return new ResponseUser { ID = id };
		})
			.WithName("GetUsersByCity")
			.WithOpenApi();

		group.MapPost("/", (RequestUser model) =>
        {
            //return TypedResults.Created($"/UserToControllers/{model.ID}", model);
        })
			.WithName("CreateUser")
			.WithOpenApi();


	}

}
