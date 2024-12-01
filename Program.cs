using Microsoft.EntityFrameworkCore;
using LAWBD_fase3.Models; // Ajuste o namespace do seu projeto

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar suporte a controladores e views (MVC)
builder.Services.AddControllersWithViews();

// Adicionar suporte a sessões
builder.Services.AddDistributedMemoryCache(); // Usando memória para armazenar sessões
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Biblioteca.Session"; // Nome do cookie
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Melhor para segurança
    options.Cookie.IsEssential = true; // O cookie é essencial para a funcionalidade da aplicação
});

var app = builder.Build();

// Configuração do pipeline de requisição
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Ativar o uso de sessões
app.UseSession();

// Habilitar o middleware de autorização, se necessário
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();