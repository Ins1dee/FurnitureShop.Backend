using FurnitureStorage.Data.Context;
using FurnitureStorage.Data.Entities;
using FurnitureStorage.Data.Infrastructure;
using FurnitureStorage.Domain.Services.Implementation;
using FurnitureStorage.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IFurnitureService, FurnitureService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<FurnitureStorageContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"), x => x.MigrationsAssembly("FurnitureStorage.Data")));
builder.Services.AddScoped<IRepository<Furniture>, Repository<Furniture>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();

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
