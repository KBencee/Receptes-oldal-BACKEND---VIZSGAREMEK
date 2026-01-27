using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReceptekWebAPI.Data;
using ReceptekWebAPI.Entities;
using ReceptekWebAPI.Models;
using System.Security.Claims;

namespace ReceptekWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptController : ControllerBase
    {
        private readonly UserDbContext _context;
        public ReceptController(UserDbContext context) => _context = context;

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recept>> Create([FromBody] ReceptDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var recept = new Recept
            {
                Id = Guid.NewGuid(),
                Nev = dto.Nev,
                Leiras = dto.Leiras ?? string.Empty,
                Hozzavalok = dto.Hozzavalok ?? string.Empty,
                ElkeszitesiIdo = dto.ElkeszitesiIdo ?? string.Empty,
                NehezsegiSzint = dto.NehezsegiSzint ?? string.Empty,
                Likes = 0,
                UserId = userId
            };

            _context.Receptek.Add(recept);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = recept.Id }, recept);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Recept>> GetById(Guid id)
        {
            var r = await _context.Receptek.FindAsync(id);
            if (r is null) return NotFound();
            return Ok(r);
        }
    }
}
