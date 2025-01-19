using AttendanceAPI.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

if (Environment.MachineName == "DAMON-PC")
{
    builder.Services.AddDbContext<AttendanceContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("LaptopAttendanceDbConnectionString")), ServiceLifetime.Singleton);
}
else
{
    builder.Services.AddDbContext<AttendanceContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("PCAttendanceDbConnectionString")), ServiceLifetime.Singleton);
}

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
