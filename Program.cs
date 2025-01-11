using Microsoft.EntityFrameworkCore;
using Prezenta_API.Services;
using Prezenta_API.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//*********************** Add services to the container.***********************
builder.Services.AddSingleton<IEntry, EntryService>();
//*********************** Add services to the container end.***********************

//*********************** Register DbContext and provide ConnectionString .***********************
builder.Services.AddDbContext<EntryContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("PrezentaConnectionString")), ServiceLifetime.Singleton);
//*********************** Register DbContext end.***********************

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

app.Run();
