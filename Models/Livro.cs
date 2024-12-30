namespace LAWBD_fase3.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public bool Disponivel { get; set; }
        public byte[] Imagem { get; set; } // Imagem do livro

        // Relacionamento com Categoria
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        // Relacionamento com Biblioteca
        public int BibliotecaId { get; set; }
        public Biblioteca Biblioteca { get; set; }
        public ICollection<Emprestimo> Emprestimos { get; set; }
    }
}