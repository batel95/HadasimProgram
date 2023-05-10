using CoronaSystem.Models;
namespace CoronaSystem.Controllers;

public static class CoronaVirusEndpoints
{
	public static void MapCoronaVirusEndpoints (this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/CoronaVirus").WithTags("CoronaVirus");


		group.MapGet ("/ills", () =>
		{
			return new [] { new ResponseUser () };
		})
		.WithName ("GetAllIllUsers")
		.WithOpenApi ();

		group.MapGet ("/", () =>
		{
			return new [] { new ResponseUser () };
		})
	   .WithName ("viewCoronaSummary")
	   .WithOpenApi ();

	}
}
