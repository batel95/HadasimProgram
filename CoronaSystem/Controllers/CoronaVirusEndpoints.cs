using CoronaSystem.Models;
using CoronaSystem.Services;

namespace CoronaSystem.Controllers;

public static class CoronaVirusEndpoints
{
	public static void MapCoronaVirusEndpoints (this IEndpointRouteBuilder routes)
	{
		var group = routes.MapGroup("/api/CoronaVirus").WithTags("CoronaVirus");


		group.MapGet ("/ills", () =>
		{
			return CoronaEndpointsService.GetAllIlls ();
		})
		.WithName ("GetAllTodayIlleUsers")
		.WithOpenApi ();

		group.MapGet ("/vaccinated", () =>
		{
			return CoronaEndpointsService.GetAllVaccinated ();
		})
			.WithName ("GetAllVaccinatedUsers")
			.WithOpenApi ();

		group.MapGet ("/view", () =>
		{
			return new [] { new ResponseUser () };
		})
	   .WithName ("viewCoronaSummary")
	   .WithOpenApi ();

	}
}
