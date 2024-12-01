using Microsoft.EntityFrameworkCore;
using LAWBD_fase3.Models; // Ajuste o namespace do seu projeto

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar suporte a controladores e views (MVC)
builder.Services.AddControllersWithViews();

// Adicionar suporte a sess�es
builder.Services.AddDistributedMemoryCache(); // Usando mem�ria para armazenar sess�es
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Biblioteca.Session"; // Nome do cookie
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
    options.Cookie.HttpOnly = true; // Melhor para seguran�a
    options.Cookie.IsEssential = true; // O cookie � essencial para a funcionalidade da aplica��o
});

var app = builder.Build();

// Configura��o do pipeline de requisi��o
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Ativar o uso de sess�es
app.UseSession();

// Habilitar o middleware de autoriza��o, se necess�rio
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();