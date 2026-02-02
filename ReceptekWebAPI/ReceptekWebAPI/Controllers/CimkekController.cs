using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReceptekWebAPI.Data;
using ReceptekWebAPI.Entities;
using ReceptekWebAPI.Models;

namespace ReceptekWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CimkekController : ControllerBase
    {
        private readonly UserDbContext _context;
        public CimkekController(UserDbContext context) => _context = context;

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Cimke>> Create([FromBody] CimkeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cimke = new Cimke { CimkeNev = dto.CimkeNev };
            _context.Cimkek.Add(cimke);
            await _context.SaveChangesAsync();

            var response = new CimkeDto { CimkeNev = cimke.CimkeNev };
            return CreatedAtAction(nameof(GetById), new { id = cimke.CimkeId }, response);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Cimke>> GetById(int id)
        {
            var c = await _context.Cimkek.FindAsync(id);
            if (c is null) return NotFound();
            var dto = new CimkeDto { CimkeNev = c.CimkeNev };
            return Ok(dto);
        }
    }
}
