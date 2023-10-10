using AutoMapper;
using BooksWebAPI.DTO;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BooksWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class CodesController
    {
        private readonly ICodeRepository _codeRepository;
        private readonly IMapper _mapper;

        public CodesController(ICodeRepository codeRepository, IMapper mapper)
        {
            _codeRepository = codeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Code>))]
        public async Task<ICollection<Code>> GetCodes() 
        {
            ICollection<Code> codes = await _codeRepository.GetCodes();

            return codes;
        }

    }
}
