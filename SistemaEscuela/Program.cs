// Program.cs
using BL;
using DAL;
using EL;
using Microsoft.EntityFrameworkCore;
using SistemaEscuela.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext (sin AddDbContext, lo manejamos manualmente para control total)
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// 2. UnitOfWork y repositorios genéricos
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 3. Servicios de negocio
builder.Services.AddScoped<EmpleadoService>();

builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<HorarioService>();

// 4. Blazor Interactive Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 5. Auth (por si acaso en el futuro)
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapFallbackToFile("index.html");

app.Run();

