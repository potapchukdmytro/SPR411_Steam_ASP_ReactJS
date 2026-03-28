using System.ComponentModel.DataAnnotations;

namespace SPR411_SteamClone.BLL.Dtos.Genre
{
    public class CreateGenreDto
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
    }
}
