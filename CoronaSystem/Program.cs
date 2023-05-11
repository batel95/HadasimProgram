using CoronaSystem.Controllers;
using CoronaSystem.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers ();
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();
builder.Services.AddDbContext<CoronaSystemDbContext> (
	   options => options.UseLazyLoadingProxies ()
	   .UseSqlServer (builder.Configuration.GetConnectionString ("CoronaSystemContext")));

var app = builder.Build();

if (app.Environment.IsDevelopment ())
{
	app.UseSwagger ();
	app.UseSwaggerUI ();
};

app.UseHttpsRedirection ();

app.UseAuthorization ();

app.MapControllers ();

app.MapUsersEndpoints ();

app.MapCoronaVirusEndpoints ();

app.EnsureDatabaseCreated ();

app.Run ();