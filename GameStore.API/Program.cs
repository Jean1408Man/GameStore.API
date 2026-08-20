using GameStore.API.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddGameStoreDB();

var app = builder.Build();

app.MapGameEndpoints();
app.MapGenreEndpoints();

app.MigrateDB();

app.Run();
