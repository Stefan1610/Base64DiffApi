using System.ComponentModel.DataAnnotations;

namespace Descarta2.Models
{
    public class DiffItemDTO
    {
        [Required]
        public int Id { get; set; }

        public diffPosition Position { get; set; }

        [Required]
        public string Data { get; set; }
    }
}

