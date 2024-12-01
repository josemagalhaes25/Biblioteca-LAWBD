namespace LAWBD_fase3.Models
{
    public class Bibliotecario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }

        public int BibliotecaId { get; set; }
        public Biblioteca Biblioteca { get; set; }
    }
}