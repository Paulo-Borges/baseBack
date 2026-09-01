using baseBack.API.Shared;
using System.Text.Json.Serialization;

namespace baseBack.API.DTOs
{
    public sealed record CadastrarPessoaRequest(
        string Nome,
        [property: JsonConverter(typeof(DataBrJsonConverter))]
        DateTime DataNascimento,
        string Cpf,
        int EstadoCivil,
        int Naturalidade,
        string Profissao,
        string Telefone,
        string Email
        );
   
}
