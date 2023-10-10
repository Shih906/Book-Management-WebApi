using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWebAPI.Models
{
    public class Member
    {
        [Key]
        [MaxLength(12)]
        [Column(TypeName = "nvarchar(12)")]
        public string Id { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? CName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? EName { get; set; }
        public DateTime? Register_Date { get; set; }

        [MaxLength(12)]
        [Column(TypeName = "nvarchar(12)")]
        public string? Register_User { get; set; }
        public DateTime? Modify_Date { get; set; }

        [MaxLength(12)]
        [Column(TypeName = "nvarchar(12)")]
        public string? Modify_User { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
