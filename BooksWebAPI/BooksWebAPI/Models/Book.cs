using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWebAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Name { get; set; }

        [MaxLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string? Author { get; set; }
        public DateTime? Bought_Date { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string? Publisher { get; set; }

        [MaxLength(1200)]
        [Column(TypeName = "nvarchar(1200)")]
        public string? Note { get; set; }
        public DateTime? Create_Date { get; set; }

        [MaxLength(12)]
        [Column(TypeName = "nvarchar(12)")]
        public string? Create_User { get; set; }
        public DateTime? Modify_Date{ get; set; }

        [MaxLength(12)]
        [Column(TypeName = "nvarchar(12)")]
        public string? Modify_User { get; set; }

        public string ClassId { get; set; }
        public Class? Class { get; set; }

        public string CodeId { get; set; }
        public Code? Code { get; set; }
        public string? MemberId { get; set; }
        public Member? Member { get; set; }

    }
}
