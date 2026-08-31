namespace baseBack.API.DTOs
{
    public sealed record CadastrarPessoaRequest(
        string Nome,
        DateTime DataNascimento,
        string Cpf,
        int EstadoCivil,
        int Naturalidade,
        string Profissao,
        string Telefone,
        string Email
        );
   
}
