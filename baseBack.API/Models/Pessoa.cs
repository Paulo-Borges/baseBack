using baseBack.API.Enums;

namespace baseBack.API.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public EstadoCivil EstadoCivil { get; set; }
        public string Profissao { get; set; } = string.Empty;
        public Naturalidade Naturalidade { get; set; }

        public Guid EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }

        public int ContatoId { get; set; }
        public Contato? Contato { get; set; }
    }
}
