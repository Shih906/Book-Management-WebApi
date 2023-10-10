using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWebAPI.Models
{
    public class Class
    {
        [Key]
        [MaxLength(4)]
        [Column(TypeName = "nvarchar(4)")]
        public string Id { get; set; }

        [MaxLength(60)]
        [Column(TypeName = "nvarchar(60)")]
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
