// Program.cs
using BL;
using DAL;
using EL;
using Microsoft.EntityFrameworkCore;
using SistemaEscuela.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext con la conexión correcta
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// 2. REPOSITORIO GENÉRICO ABIERTO → ¡¡ESTO ES LO QUE TE FALTABA!!
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 3. Servicios de negocio (podes agregar más cuando quieras)
builder.Services.AddScoped<EmpleadoService>();
// builder.Services.AddScoped<DepartamentoService>();
// builder.Services.AddScoped<HorarioService>();
// etc...

// 4. Blazor con Interactive Server (tu caso)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 5. (Opcional pero recomendado) Authentication & Authorization
// Aunque no uses login aún, no hace daño y evita errores futuros
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline obligatorio
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ¡¡IMPORTANTE!! Estos dos SIEMPRE van en este orden
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// Mapeo de Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Endpoint fallback (opcional pero recomendado)
app.MapFallbackToFile("index.html");

app.Run();


