using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptekWebAPI.Data;
using ReceptekWebAPI.Entities;
using ReceptekWebAPI.Models;
using System.Security.Claims;

namespace ReceptekWebAPI.Controllers
{
    [Route("api/saved")]
    [ApiController]
    [Authorize]
    public class MentettReceptekController : ControllerBase
    {
        private readonly UserDbContext _context;

        public MentettReceptekController(UserDbContext context)
        {
            _context = context;
        }

        [HttpPost("/saved/{receptId:guid}")]
        public async Task<IActionResult> ReceptMentese(Guid receptId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
            {
                return Unauthorized();
            }

            var letezik = await _context.MentettReceptek
                .AnyAsync(mr => mr.UserId == userId && mr.ReceptId == receptId);

            if (letezik)
                return Conflict("Ez a recept már mentve van.");

            var receptLetezik = await _context.Receptek
                .AnyAsync(r => r.Id == receptId);

            if (!receptLetezik)
                return NotFound("Ez a recept nem létezik.");

            _context.MentettReceptek.Add(new MentettRecept
            {
                UserId = userId,
                ReceptId = receptId,
                MentveEkkor = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("/saved/{receptId:guid}")]
        public async Task<IActionResult> MentettReceptTorlese(Guid receptId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var mentve = await _context.MentettReceptek
                .FirstOrDefaultAsync(sr => sr.UserId == userId && sr.ReceptId == receptId);

            if (mentve is null)
                return NotFound();

            _context.MentettReceptek.Remove(mentve);
            await _context.SaveChangesAsync();

            return NoContent();
            
        }

        [HttpGet("/api/user/me/saved")]
        [Authorize]
        public async Task<ActionResult<List<MentettReceptResponseDto>>> GetMySaved()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var mentve = await _context.MentettReceptek
                .Where(mr => mr.UserId == userId)
                .Include(mr => mr.Recept)
                .OrderByDescending(mr => mr.MentveEkkor)
                .Select(mr => new MentettReceptResponseDto
                {
                    ReceptId = mr.ReceptId,
                    Nev = mr.Recept.Nev,
                    Likes = mr.Recept.Likes,
                    MentveEkkor = mr.MentveEkkor,
                    KepUrl = mr.Recept.KepUrl
                })
                .ToListAsync();

            return Ok(mentve);
        }
    }
}
