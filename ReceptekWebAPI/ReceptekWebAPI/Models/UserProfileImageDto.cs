using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;    

namespace ReceptekWebAPI.Models
{
    public class UserProfileImageDto
    {
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
