using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SPR411_SteamClone.BLL.Dtos.Game
{
    public class CreateGameDto
    {
        [Required]
        public required string Name { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int DeveloperId { get; set; }
        public List<string> Genres { get; set; } = [];
        public IFormFile? PreviewImage { get; set; }
        public List<IFormFile> Images { get; set; } = [];
    }
}
