using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission6.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1888, 2025)]
        public string Year { get; set; }

        public string? Director { get; set; }
        public string? Rating { get; set; }

        public bool CopiedToPlex { get; set; } = false;

        public bool Edited { get; set; }  = false;

        public string? LentTo { get; set; }
        public string? Notes { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // ✅ Make navigation property nullable to avoid loading issues
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }  
    }
}