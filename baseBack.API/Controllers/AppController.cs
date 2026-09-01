using baseBack.API.DataContext;
using baseBack.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using baseBack.API.DTOs;
using System.Reflection.Metadata.Ecma335;
using baseBack.API.Enums;

namespace baseBack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoResponse>>> GetContatos( CancellationToken cancellationToken)
        {
            var contatos = await _context.Contatos
                .AsNoTracking()
                .Select(contato => new ContatoResponse(
                    contato.Id,
                    contato.Nome,
                    contato.Email,
                    contato.Mensagem))
                .ToListAsync(cancellationToken);


            return Ok(contatos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _context.Contatos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contato is null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ContatoResponse>> UpdateContato( int id, [FromBody] Contato editarContato)
        {
            // 1. Busca o registro existente no banco de dados pelo ID
            var contatoExistente = await _context.Contatos.FindAsync(id);

            if (contatoExistente == null)
            {
                return NotFound("Contato não encontrado.");
            }

            contatoExistente.Nome = editarContato.Nome;
            contatoExistente.Email = editarContato.Email;
            contatoExistente.Mensagem = editarContato.Mensagem;


            await _context.SaveChangesAsync();

            var response = new ContatoResponse(
              contatoExistente.Id,
              contatoExistente.Nome,
              contatoExistente.Email,
              contatoExistente.Mensagem
             );


            return (response);
        }

        [HttpPost]
        public async Task<ActionResult<ContatoResponse>> CreateContato(CreateContatoRequest request, CancellationToken cancellationToken)
        {
            var contato = new Contato
            {
                Nome = request.Nome,
                Email = request.Email,
                Mensagem = request.Mensagem
            };
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync(cancellationToken);

            var contatoResponse = new ContatoResponse(contato.Id, contato.Nome, contato.Email, contato.Mensagem);
            return CreatedAtAction(nameof(GetContatos), new { id = contato.Id }, contatoResponse);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteContato(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);

            if (contato == null)
            {
                return NotFound("Contato não encontrado.");
            }

            _context.Contatos.Remove(contato);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
    [ApiController]
    [Route("api/pessoa")]

    public class PessoaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PessoaController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOPessoa>>> GetPessoas()
        {
            var pessoa = await _context.Pessoas
                .AsNoTracking()
                .Select(pessoa => new DTOPessoa(
                    pessoa.Id,
                    pessoa.Nome,
                    pessoa.Cpf,
                    pessoa.Email))
                .ToListAsync();

            return Ok(pessoa);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DTOPessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new DTOPessoa(p.Id, p.Nome, p.Cpf, p.Email))
                .FirstOrDefaultAsync();

            if (pessoa is null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<DTOPessoa>> Cadastrar([FromBody] CadastrarPessoaRequest request)
        {
            var pessoa = new Pessoa
            {
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                Cpf = request.Cpf,
                Email = request.Email,
                Telefone = request.Telefone,
                EstadoCivil = (EstadoCivil)request.EstadoCivil,
                Profissao = request.Profissao,
                Naturalidade = (Naturalidade)request.Naturalidade,

                 Contato = new Contato
                 {
                     Nome = request.Nome,
                     Email = request.Email,    
                 }
            };
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletarPessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
