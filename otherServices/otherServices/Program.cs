using Microsoft.EntityFrameworkCore;
using otherServices.Data_Project;
using otherServices.Data_Project.Models;
using otherServices.Data_Project.service;

//using otherServices.Data.Model;
//using otherServices.Data_Project.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("constr");
builder.Services.AddDbContext<AppDbContext2>(option =>
    option.UseSqlServer(connectionString)
);

builder.Services.AddScoped<KafkaProducerService>(); //for producer
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
