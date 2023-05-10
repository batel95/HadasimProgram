using CoronaSystem.Data;

using Microsoft.EntityFrameworkCore;
using CoronaSystem.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CoronaSystemDbContext>( 
	   options => options.UseLazyLoadingProxies()
	   .UseSqlServer(builder.Configuration.GetConnectionString("CoronaSystemContext")));


var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapUsersEndpoints();

app.MapCoronaVirusEndpoints();


app.Run();

Console.WriteLine("h");
