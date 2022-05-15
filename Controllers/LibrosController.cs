using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ILogger<LibrosController> _logger;
        private readonly ApplicationDbContext _context;

        public LibrosController(ILogger<LibrosController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await _context.Libros.Include(q => q.Autor).FirstOrDefaultAsync(q => q.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await _context.Autores.AnyAsync(q => q .Id == libro.AutorId);
            if(!existeAutor)
            {
                return BadRequest($"No existe el autor del Id: {libro.AutorId}");
            }
            _context.Add(libro);
            await _context.SaveChangesAsync();
            return Ok();
        }        
    }
}