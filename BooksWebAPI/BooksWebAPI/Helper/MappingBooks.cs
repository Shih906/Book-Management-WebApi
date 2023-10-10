using AutoMapper;
using BooksWebAPI.DTO;
using BooksWebAPI.Models;

namespace BooksWebAPI.Helper
{
    public class MappingBooks : Profile
    {
        public MappingBooks()
        {
            CreateMap<Book, BookDto>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.password, input => input.MapFrom(i => i.PasswordHash))
                .ReverseMap();
            //CreateMap<Book, BookDto>()
            //.ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
            //.ForMember(dest => dest.CodeName, opt => opt.MapFrom(src => src.Code.Name));
        }
    }
}
