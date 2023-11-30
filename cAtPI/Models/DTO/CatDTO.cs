using System.ComponentModel.DataAnnotations;

namespace cAtPI.Models.DTO
{
    public class CatDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public bool? IsCute { get; set; }
    }
}
