using AutoMapper;
using BooksWebAPI.DTO;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using BooksWebAPI.Repository;
using BooksWebAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BooksWebAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    [Produces("application/json")]
    
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration; 
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getUsers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            ICollection<User> users = await _userRepository.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }
        

        [HttpGet]
        [Route("getUserProfile")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserProfile()
        {
            //JWT 驗證通過後，可取得先前配置在 Claim 中的 Name
            string userName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("userName is null");
            }

            User user = await _userRepository.GetUserByName(userName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.IsUserExists(userDto.Id))
            {
                return BadRequest("User is not exist");
            }

            User userMap = _mapper.Map<User>(userDto);
            userMap.PasswordHash = await _userRepository.GetUserPassHash(userMap.Id);
            userMap.UpdateTime = DateTime.Now;

            if (!await _userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("Save", "Saving user error");
                StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            try
            {
                User userMap = _mapper.Map<User>(userDto);
                string passwordHash = Hash.hashPassword(userMap.PasswordHash);
                User dbUser = await _userRepository.GetUser(userMap.Username, passwordHash);
                if (dbUser == null)
                {
                    return Unauthorized("使用者名稱或密碼不正確!");
                }

                string token = CreateToken(userMap);

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                User userMap = _mapper.Map<User>(userDto);
                bool isExist = await _userRepository.IsUserExist(userMap.Username);
                if (isExist)
                {
                    return BadRequest("使用者名稱已存在!");
                }

                userMap.PasswordHash = Hash.hashPassword(userMap.PasswordHash);
                userMap.UpdateTime = DateTime.Now;

                var result = await _userRepository.CreateUser(userMap);

                if (result != 1)
                {
                    return BadRequest("註冊失敗!");
                }
                return Ok("註冊成功!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWT:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now,
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return "Bearer " + jwt;
        }
    }
}
