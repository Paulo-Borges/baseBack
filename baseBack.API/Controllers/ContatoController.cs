using baseBack.API.DataContext;
using baseBack.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetContatos()
        {
            var contatos = _context.Contatos.ToList();
            return Ok(contatos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetContato(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato is null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        [HttpPost]
        public IActionResult CreateContato([FromBody] Contato contato)
        {
            if (contato == null)
            {
                return BadRequest();
            }
            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetContatos), new { id = contato.Id }, contato);
        }
    }
}
