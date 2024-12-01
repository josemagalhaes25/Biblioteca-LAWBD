namespace LAWBD_fase3.Models
{
    public class Biblioteca
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }

        // Relacionamentos
        public ICollection<Bibliotecario> Bibliotecarios { get; set; }
        public ICollection<Livro> Livros { get; set; }
    }
}