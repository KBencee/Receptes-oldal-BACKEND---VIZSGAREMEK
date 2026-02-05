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
    [Route("api/[kepek]")]
    [ApiController]
    [Authorize]
    public class KepekController : ControllerBase
    {
        private readonly ImageKitService _imageKitService;
        private readonly UserDbContext _context;

        public KepekController(ImageKitService imageKitService, UserDbContext context)
        {
            _imageKitService = imageKitService;
            _context = context;
        }

        [HttpPost("upload/{receptId:guid}")]
        public async Task<ActionResult<KepResponseDto>> Upload(
            Guid receptId,
            [FromForm] UploadKepDto dto)
        {
            var recept = await _context.Receptek.FindAsync(receptId);
            if (recept is null)
                return NotFound("Recept nem létezik");

            using var ms = new MemoryStream();
            await dto.File.CopyToAsync(ms);

            var result = _imageKitService.Upload(ms.ToArray(), dto.File.FileName);

            if (result == null || result.url == null)
                return StatusCode(500, "Kép feltöltés sikertelen");

            var kep = new FeltoltottKep
            {
                Id = Guid.NewGuid(),
                Url = result.url,
                FileId = result.fileId,
                ReceptId = receptId
            };

            _context.FeltoltottKepek.Add(kep);
            await _context.SaveChangesAsync();

            return Ok(new KepResponseDto
            {
                KepId = kep.Id,
                Url = kep.Url
            });
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var recept = await _context.Receptek
                .Include(r => r.Kep)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (recept is null)
                return NotFound();

            // 🗑 ImageKit törlés
            if (recept.Kep != null)
            {
                _imageKitService.Delete(recept.Kep.FileId);
            }

            _context.Receptek.Remove(recept);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
