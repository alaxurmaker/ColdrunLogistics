using ColdrunLogistics.Core.Interfaces;
using ColdrunLogistics.Core.Services;
using ColdrunLogistics.Data;
using ColdrunLogistics.Data.Models.Trucks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMemoryCache();

builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<ITruckRepository, TruckRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
