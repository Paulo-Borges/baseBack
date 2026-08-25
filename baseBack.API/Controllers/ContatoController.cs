using baseBack.API.DataContext;
using baseBack.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using baseBack.API.DTOs;

namespace baseBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContatoController(AppDbContext context)
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

        [HttpGet("demorado")]
        public async Task<IActionResult> Demorado( CancellationToken cancellationToken )
        {
            await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);
            return Ok(new { mensagem = "Operacao concluida" });
        }
    }
}
