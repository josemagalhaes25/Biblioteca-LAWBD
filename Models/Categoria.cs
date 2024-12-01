namespace LAWBD_fase3.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Relacionamentos
        public ICollection<Livro> Livros { get; set; }
    }
}