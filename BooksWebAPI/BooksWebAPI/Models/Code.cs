using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BooksWebAPI.Models
{
    public class Code
    {
        [Key]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Id { get; set; }

        [MaxLength(60)]
        [Column(TypeName = "nvarchar(60)")]
        public string Type { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Type_Desc { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }
        public DateTime? Create_Date { get; set; }

        [MaxLength(12)]
        [Column(TypeName = "nvarchar(12)")]
        public string? Create_User { get; set; }
        public DateTime? Modify_Date { get; set; }

        [MaxLength(12)]
        [Column(TypeName = "nvarchar(12)")]
        public string? Modify_User { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
