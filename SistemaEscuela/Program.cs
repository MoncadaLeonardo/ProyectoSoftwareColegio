using BL;
using DAL;
using EL;
using Microsoft.EntityFrameworkCore;
using SistemaEscuela.Components;

var builder = WebApplication.CreateBuilder(args);

// Registrar el DbContext usando la cadena de conexión del appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorio genérico
builder.Services.AddScoped<GenericRepository<Empleado>>();

// Registrar servicios de negocio
builder.Services.AddScoped<EmpleadoService>();

// Servicios de Razor Components interactivos
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAntiforgery();


app.MapStaticAssets();


app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();



