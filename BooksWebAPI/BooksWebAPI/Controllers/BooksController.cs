using AutoMapper;
using AutoMapper.Execution;
using BooksWebAPI.DBUtility;
using BooksWebAPI.DTO;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using BooksWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles ="Admin")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = _bookRepository.GetBooks();
            return Json(books);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public async Task<IActionResult> GetBooks()
        {
            ICollection<Book> books = await _bookRepository.GetBooks();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(books);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public async Task<IActionResult> GetBookRltData()
        {
            ICollection<Book> books = await _bookRepository.GetBookRltData();
            // 映射到 DTO 對象
            List<BookDto> bookDtos = _mapper.Map<List<BookDto>>(books);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookDtos);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public async Task<IActionResult> GetBookPaging(int pageNumber, int pageSize, string? classId, string? codeId)
        {
            ICollection<Book> books = await _bookRepository.GetBookRltData();
            IQueryable<Book> filteredBooks = books.AsQueryable();

            if (!string.IsNullOrEmpty(classId) && !string.IsNullOrEmpty(codeId))
            {
                filteredBooks = filteredBooks.Where(book => book.ClassId == classId && book.CodeId == codeId);
            }
            else if (!string.IsNullOrEmpty(classId) && string.IsNullOrEmpty(codeId))
            {
                filteredBooks = filteredBooks.Where(book => book.ClassId == classId);
            }
            else if (string.IsNullOrEmpty(classId) && !string.IsNullOrEmpty(codeId))
            {
                filteredBooks = filteredBooks.Where(book => book.CodeId == codeId);
            }
            
            List<BookDto> bookDtos = _mapper.Map<List<BookDto>>(filteredBooks.Skip((pageNumber - 1) * pageSize).Take(pageSize));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookDtos);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        //[ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBook(int id)
        {
            if (!_bookRepository.IsBookExists(id))
                return NotFound();

            Book book = await _bookRepository.GetBook(id);
            BookDto bookDto = _mapper.Map<BookDto>(book);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest(ModelState);
            }
            //JWT 驗證通過後，可取得先前配置在 Claim 中的 Name
            string userName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            Book insertData = new Book()
            {
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Note = book.Note,
                Create_Date = DateTime.Now,
                Create_User = "Admin",
                Modify_Date = DateTime.Now,
                Modify_User = userName,
                ClassId = book.ClassId,
                CodeId = book.CodeId,
                MemberId = string.IsNullOrEmpty(book.MemberId) ? null : book.MemberId
            };

            if (!_bookRepository.CreateBook(insertData))
            {
                ModelState.AddModelError("Save", "Saving book error");
                StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest(ModelState);
            }

            if (!_bookRepository.IsBookExists(book.Id))
            {
                return BadRequest("Book is not exist");
            }
            //JWT 驗證通過後，可取得先前配置在 Claim 中的 Name
            string userName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            Book updateData = new Book()
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Publisher = book.Publisher,
                Note = book.Note,
                Create_Date = DateTime.Now,
                Create_User = "Admin",
                Modify_Date = DateTime.Now,
                Modify_User = userName,
                ClassId = book.ClassId,
                CodeId = book.CodeId,
                MemberId = string.IsNullOrEmpty(book.MemberId) ? null : book.MemberId
            };

            if (!_bookRepository.UpdateBook(updateData))
            {
                ModelState.AddModelError("Save", "Saving book error");
                StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            if(!_bookRepository.IsBookExists(bookId))
            {
                return NotFound();
            }

            Book bookDelete = await _bookRepository.GetBook(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 返回包含驗證錯誤的 BadRequest 錯誤回應
            }

            if (!_bookRepository.DeleteBook(bookDelete))
            {
                ModelState.AddModelError("","刪除失敗");
            }

            return Ok("刪除成功");

        }

    }
}
