using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Mappings;
using NZWalksAPI.Models.Data;
using NZWalksAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configurations = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(configurations["ConnectionStrings:NZWalksConnectionString"]));
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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

