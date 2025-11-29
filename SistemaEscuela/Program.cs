using BL;
using BL.Services.Auth;
using DAL;
using EL;
using Microsoft.EntityFrameworkCore;
using SistemaEscuela.Components;
using Microsoft.AspNetCore.Authentication.Cookies; // Necesario para CookieAuthenticationDefaults
using Microsoft.AspNetCore.Authentication; // Necesario para SignInAsync y SignOutAsync
using System.Security.Claims; // Necesario para ClaimsPrincipal

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// 2. Autenticación por cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromDays(14);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddAuthorization();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// 3. HttpContextAccessor (OBLIGATORIO para SignInAsync en Blazor Server)
builder.Services.AddHttpContextAccessor();

// 4. Tu AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// 5. UnitOfWork y repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 6. Servicios de negocio
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<HorarioService>();
builder.Services.AddScoped<AulaService>();
builder.Services.AddScoped<SeccionService>();
builder.Services.AddScoped<RolService>();

// 7. Blazor (.NET 9 con <Routes />)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

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

// --- Handlers de Autenticación (Minimal APIs) ---

// ===============================================
// 🚨 HANDLER DE LOGIN
// ===============================================
app.MapPost("/login-handler", async (
    HttpContext context,
    IAuthService authService) =>
{
    var form = await context.Request.ReadFormAsync();
    var userName = form["UserName"].ToString() ?? "";
    var password = form["Password"].ToString() ?? "";

    // 1. Validar usuario
    var usuario = await authService.ValidateUserAsync(userName, password);

    if (usuario == null)
    {
        // Fallo: Redirige de vuelta con mensaje de error
        return Results.Redirect("/login?ErrorMessage=Usuario+o+contraseña+incorrectos");
    }

    // 2. Crear Principal y SignIn
    var principal = await authService.CreateUserPrincipalAsync(usuario);

    await context.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal,
        new AuthenticationProperties { IsPersistent = true });

    // 3. Redirigir al usuario al Home
    return Results.Redirect("/");

}).AllowAnonymous(); // Permite el acceso para poder logear.

// ===============================================
// 🚀 HANDLER DE LOGOUT (NUEVO CÓDIGO)
// ===============================================
app.MapPost("/logout-handler", async (HttpContext context) =>
{
    // 1. Llama a SignOutAsync para eliminar la cookie del navegador.
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    // 2. Redirige al usuario al Login.
    return Results.Redirect("/login", permanent: false);
});
// ===============================================

// Map Blazor (.NET 9 con HeadOutlet @rendermode InteractiveServer)
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// ========================================
// CREAR USUARIO ADMIN AUTOMÁTICAMENTE
// ========================================
using var scope = app.Services.CreateScope();
var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

try
{
    await authService.RegisterAsync(
        idEmpleado: 1,
        userName: "admin",
        password: "Admin123!",
        idRol: 1
    );
    Console.WriteLine("Usuario admin creado correctamente");
}
catch
{
    Console.WriteLine("El usuario admin ya existe o hubo un error");
}

app.Run();