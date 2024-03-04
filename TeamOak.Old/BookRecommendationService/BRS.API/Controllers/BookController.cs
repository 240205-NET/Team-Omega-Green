using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRS.Data;
using BRS.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BRS.API.Controllers
{
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        //Fields
        private readonly IBookRepository _repo;

        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository repo, ILogger<BookController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }
        
        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksAsync()
        {
            IEnumerable<Book> books;
            try
            {
                books = await _repo.GetAllBooksAsync();
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return books.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
        {
            Book book;
            try
            {
                book = await _repo.GetBookAsyncById(id);
            }
            catch(Exception e) {

                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return book;
        }

        // GET api/values/5
        [HttpGet("[action]/{isbn}")]
        public async Task<ActionResult<Book>> GetBookByIsbn(string isbn)
        {
            Book book;
            try
            {
                book = await _repo.GetBookByIsbnAsync(isbn);
            }
            catch (Exception e)
            {

                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return book;
        }
        [HttpGet("get-book/{title}")]
        public async Task<ActionResult<Book>> GetBookByTitleAsync(string title)
        {
            Book book;
            try
            {
                book = await _repo.GetBookByTitleAsync(title);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            return book;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]Book book)
        {
            try
            {
                await _repo.AddBook(book);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

    }
}

