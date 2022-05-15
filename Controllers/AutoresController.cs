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
    public class AutoresController : ControllerBase
    {
        private readonly ILogger<AutoresController> _logger;
        private readonly ApplicationDbContext _context;

        public AutoresController(ILogger<AutoresController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            return await _context.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            var existe = await _context.Autores.AnyAsync(q => q.Id == id);
            if(!existe)
            {
                return NotFound();
            }

            if(autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el de la URL");
            }
            _context.Update(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }        

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Autores.AnyAsync(q => q.Id == id);
            if(!existe)
            {
                return NotFound();
            }

            _context.Autores.Remove(new Autor() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}