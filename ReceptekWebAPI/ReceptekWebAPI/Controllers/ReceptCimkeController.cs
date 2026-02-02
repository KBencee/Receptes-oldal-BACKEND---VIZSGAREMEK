using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptekWebAPI.Data;
using ReceptekWebAPI.Entities;
using ReceptekWebAPI.Models;

namespace ReceptekWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptCimkeController : ControllerBase
    {
        private readonly UserDbContext _context;
        public ReceptCimkeController(UserDbContext context) => _context = context;

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReceptCimke>> Create([FromBody] ReceptCimkeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var receptExists = await _context.Receptek.AnyAsync(r => r.Id == dto.ReceptId);
            var cimkeExists = await _context.Cimkek.AnyAsync(c => c.CimkeId == dto.CimkeId);
            if (!receptExists || !cimkeExists)
                return BadRequest("Recept or Cimke not found.");

            var existing = await _context.ReceptCimkek.FindAsync(dto.ReceptId, dto.CimkeId);
            if (existing is not null) return Conflict("Relation already exists.");

            var entity = new ReceptCimke
            {
                ReceptId = dto.ReceptId,
                CimkeId = dto.CimkeId
            };

            _context.ReceptCimkek.Add(entity);
            await _context.SaveChangesAsync();

            var response = new ReceptCimkeDto { ReceptId = entity.ReceptId, CimkeId = entity.CimkeId };
            return CreatedAtAction(nameof(GetByIds), new { receptId = entity.ReceptId, cimkeId = entity.CimkeId }, entity);
        }

        [HttpGet("{receptId:guid}/{cimkeId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ReceptCimke>> GetByIds(Guid receptId, int cimkeId)
        {
            var rc = await _context.ReceptCimkek.FindAsync(receptId, cimkeId);
            if (rc is null) return NotFound();
            var dto = new ReceptCimkeDto { ReceptId = rc.ReceptId, CimkeId = rc.CimkeId };
            return Ok(dto);
        }
    }
}
