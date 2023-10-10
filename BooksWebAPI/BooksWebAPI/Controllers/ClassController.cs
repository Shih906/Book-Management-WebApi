using AutoMapper;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using BooksWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BooksWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class ClassController
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public ClassController(IClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Class>))]
        public async Task<ICollection<Class>> GetClasses()
        {
            ICollection<Class> classes = await _classRepository.GetClasses();

            return classes;
        }
    }
}
