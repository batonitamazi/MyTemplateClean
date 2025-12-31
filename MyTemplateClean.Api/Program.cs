using MyTemplateClean.Api;
using MyTemplateClean.Api.Apis;
using MyTemplateClean.Application;
using MyTemplateClean.BuildingBlocks.Extensions;
using MyTemplateClean.Infrastructure;
using MyTemplateClean.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

app.UseDefaultOpenApi();  

app.UseApiServices();      

app.MapDefaultEndpoints();

var todos = app.NewVersionedApi("Todos");
todos.MapTodosApiV1();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();