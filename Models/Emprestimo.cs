namespace LAWBD_fase3.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public int LeitorId { get; set; }
        public int LivroId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        // Relacionamentos
        public Leitor Leitor { get; set; }
        public Livro Livro { get; set; }
    }
}