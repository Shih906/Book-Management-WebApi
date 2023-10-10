namespace BooksWebAPI.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? username { get; set; }

        public string? password { get; set; } = null!;

        public string? ImageBase64 { get; set; }

        public char? Gender { get; set; }

        public string? Email { get; set; }

        public DateTime? Birthday { get; set; }


    }
}
