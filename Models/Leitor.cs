namespace LAWBD_fase3.Models
{
    public class Leitor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Password { get; set; }

        // Relacionamentos
        public ICollection<Emprestimo> Emprestimos { get; set; }
    }
}