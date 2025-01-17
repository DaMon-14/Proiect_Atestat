using Attendance.Services;
using Attendance.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICard, CardService>();
builder.Services.AddSingleton<IClient, ClientService>();
builder.Services.AddSingleton<ICourse, CourseService>();
builder.Services.AddSingleton<IClient_Course, Client_CourseService>();
builder.Services.AddSingleton<IScanner, ScannerService>();

if (Environment.MachineName == "DAMON-PC")
{
    builder.Services.AddDbContext<AttendanceContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("LaptopAttendanceDbConnectionString")), ServiceLifetime.Singleton);
}
else
{
    builder.Services.AddDbContext<AttendanceContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("PCAttendanceDbConnectionString")), ServiceLifetime.Singleton);
}


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
