using ArticulosAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ArticulosAPI;
using ArticulosAPI.Repository;
using ArticulosAPI.Repositorios;
using Microsoft.CodeAnalysis.Host.Mef;
using ArticulosAPI.DTO;
using ArticulosAPI.Modelos;
using MongoDB.Driver;
using System.Configuration;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ArticulosDataBaseSettings>(
    builder.Configuration.GetSection("ArticulosDB"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IConfiguration>()
        .GetSection("ArticulosDB")
        .Get<ArticulosDataBaseSettings>();

    return new MongoClient(settings.ConnectionString);
});


builder.Services.AddControllers();  
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IArticulosRepositorio, ArticuloRepositorio>();
builder.Services.AddScoped<ResponseDTO>();


