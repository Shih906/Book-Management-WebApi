using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWebAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string Username { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string PasswordHash { get; set; }

        public string? ImageBase64 { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "char(1)")]
        public char? Gender { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Email { get; set; }

        [MaxLength(255)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Birthday { get; set; }

        public DateTime? UpdateTime { get; set; }

    }
}
