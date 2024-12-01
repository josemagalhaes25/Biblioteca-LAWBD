using Microsoft.EntityFrameworkCore;

namespace LAWBD_fase3.Models
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options) { }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Biblioteca> Bibliotecas { get; set; }
        public DbSet<Bibliotecario> Bibliotecarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Leitor> Leitores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de relacionamentos para Emprestimos
            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Leitor)
                .WithMany(l => l.Emprestimos)
                .HasForeignKey(e => e.LeitorId);

            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Livro)
                .WithMany(l => l.Emprestimos)
                .HasForeignKey(e => e.LivroId);

            // Configuração para Livro
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(l => l.CategoriaId);

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Biblioteca)
                .WithMany(b => b.Livros)
                .HasForeignKey(l => l.BibliotecaId);

            // Configuração para Bibliotecario
            modelBuilder.Entity<Bibliotecario>()
                .HasOne(b => b.Biblioteca)
                .WithMany(b => b.Bibliotecarios)
                .HasForeignKey(b => b.BibliotecaId);
        }
    }
}