
using Microsoft.EntityFrameworkCore;
using PayRollSystem.Api.Extensions;
using PayRollSystem.Api.Extentions;
using PayRollSystem.Data.Context;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;



builder.Services.ConfigureIdentity();
builder.Services.ConfigureMailService(config);
builder.Services.AddSwagger();
builder.Services.ConfigureAutoMappers();
builder.Services.ConfigureAuthentication(config);
builder.Services.AddDependencyInjection();
builder.Services.AddDbContext<PayRollSystemContext>(options => options.UseSqlServer(config.GetConnectionString("ConnStr")));
// Add services to the container.
builder.Services.AddAuthentication();

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
