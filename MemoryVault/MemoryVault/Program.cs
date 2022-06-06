using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MemoryVault.Data;
using MemoryVault.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MemoryVaultContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MemoryVaultContext") ?? throw new InvalidOperationException("Connection string 'MemoryVaultContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseVaultValidation();

app.Run();
