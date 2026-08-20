using GameStore.Api.Data;
using GameStore.API.Endpoints;
using GameStore.API.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddGameStoreDB();

var app = builder.Build();
app.MapGameEndpoints();

app.MigrateDB();

app.Run();
