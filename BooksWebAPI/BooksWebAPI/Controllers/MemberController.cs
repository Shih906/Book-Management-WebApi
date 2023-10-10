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
    public class MemberController
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberController(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Member>))]
        public async Task<ICollection<Member>> GetMembers()
        {
            ICollection<Member> members = await _memberRepository.GetMembers();

            return members;
        }
    }
}
