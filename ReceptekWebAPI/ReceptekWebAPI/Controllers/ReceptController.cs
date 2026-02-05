using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptekWebAPI.Data;
using ReceptekWebAPI.Entities;
using ReceptekWebAPI.Models;
using ReceptekWebAPI.Services;
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
        public async Task<ActionResult<ReceptResponseDto>> Create([FromBody] ReceptDto dto)
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
                ElkeszitesiIdo = dto.ElkeszitesiIdo,
                NehezsegiSzint = dto.NehezsegiSzint ?? string.Empty,
                Likes = 0,
                UserId = userId,
                FeltoltveEkkor = DateTime.UtcNow
            };

            _context.Receptek.Add(recept);

            if (dto.CimkeIds != null && dto.CimkeIds.Any())
            {
                var uniqueIds = dto.CimkeIds.Distinct().ToList();

                var existingTagIds = await _context.Cimkek
                    .Where(c => uniqueIds.Contains(c.CimkeId))
                    .Select(c => c.CimkeId)
                    .ToListAsync();

                if (existingTagIds.Count != uniqueIds.Count)
                {
                    return BadRequest("One or more provided CimkeIds do not exist.");
                }

                foreach (var cimkeId in uniqueIds)
                {
                    _context.ReceptCimkek.Add(new ReceptCimke
                    {
                        ReceptId = recept.Id,
                        CimkeId = cimkeId
                    });
                }
            }

            await _context.SaveChangesAsync();

            var r = await _context.Receptek
                .Include(x => x.ReceptCimkek).ThenInclude(rc => rc.Cimke)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == recept.Id);

            var response = new ReceptResponseDto
            {
                Id = r!.Id,
                Nev = r.Nev,
                Leiras = r.Leiras,
                Hozzavalok = r.Hozzavalok,
                ElkeszitesiIdo = r.ElkeszitesiIdo,
                NehezsegiSzint = r.NehezsegiSzint,
                Likes = r.Likes,
                FeltoltoUsername = r.User?.Username ?? "Unknown",
                FeltoltveEkkor = r.FeltoltveEkkor,
                KepUrl = r.Kep != null ? r.Kep.Url : null,
                Cimkek = r.ReceptCimkek.Select(rc => rc.Cimke.CimkeNev).ToList(),
                MentveVan = false
            };

            return CreatedAtAction(nameof(GetById), new { id = recept.Id }, response);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ReceptResponseDto>> GetById(Guid id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid? userId = Guid.TryParse(userIdStr, out var u) ? u : null;

            var r = await _context.Receptek
            .Include(r => r.User)
            .Include(r => r.Kep)
            .Include(r => r.ReceptCimkek)
            .ThenInclude(rc => rc.Cimke)
            .FirstOrDefaultAsync(r => r.Id == id);

            if (r is null) return NotFound();
            
            var mentveVan = userId != null && await _context.MentettReceptek
                .AnyAsync(mr => mr.ReceptId == r.Id && mr.UserId == userId);

            var response = new ReceptResponseDto
            {
                Id = r.Id,
                Nev = r.Nev,
                Leiras = r.Leiras,
                Hozzavalok = r.Hozzavalok,
                ElkeszitesiIdo = r.ElkeszitesiIdo,
                NehezsegiSzint = r.NehezsegiSzint,
                Likes = r.Likes,
                FeltoltoUsername = r.User?.Username ?? "Unknown",
                FeltoltveEkkor = r.FeltoltveEkkor,
                KepUrl = r.Kep != null ? r.Kep.Url : null,
                Cimkek = r.ReceptCimkek.Select(rc => rc.Cimke.CimkeNev).ToList(),
                MentveVan = mentveVan
            };

            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ReceptResponseDto>>> GetAll()
        {
            var recepts = await _context.Receptek
                .Include(r => r.ReceptCimkek)
                    .ThenInclude(rc => rc.Cimke)
                .Include(r => r.User)
                .ToListAsync();

            var responses = recepts.Select(r => new ReceptResponseDto
            {
                Id = r.Id,
                Nev = r.Nev,
                Leiras = r.Leiras,
                Hozzavalok = r.Hozzavalok,
                ElkeszitesiIdo = r.ElkeszitesiIdo,
                NehezsegiSzint = r.NehezsegiSzint,
                Likes = r.Likes,
                FeltoltoUsername = r.User?.Username ?? "Unknown",
                FeltoltveEkkor = r.FeltoltveEkkor,
                KepUrl = r.Kep != null ? r.Kep.Url : null,
                Cimkek = r.ReceptCimkek.Select(rc => rc.Cimke.CimkeNev).ToList(),
                MentveVan = false
            }).ToList();

            return Ok(responses);
        }
    }
}
