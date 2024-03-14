using Microsoft.AspNetCore.Mvc;
using Lit.Server.Data;
using Microsoft.EntityFrameworkCore;
using Lit.Server.Logic;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
	private readonly ApplicationContext _context;
	private readonly IConfiguration _configuration;
	// public BooksController(ApplicationContext context)
	// {
	// 	_context = context;
	// }

	private readonly ILogger<BooksController> _logger;
	public BooksController(ILogger<BooksController> logger, ApplicationContext context, IConfiguration configuration)
	{
		_logger = logger;
		_context = context;
		_configuration = configuration;
	}
	// // GET: api/Books
	// [HttpGet]
	// public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
	// {
	// 	return await _context.Books.ToListAsync();
	// }
	public class BookDTO
	{
		public string Isbn { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int CategoryId { get; set; }
		// Optionally include category details if needed
		public string CategoryName { get; set; }
	}

	// When querying and returning data from your controller
	[HttpGet]
	public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
	{
		var books = await _context.Books
			.Include(book => book.Category) // Eagerly load Category
			.Select(book => new BookDTO
			{
				Isbn = book.Isbn,
				Title = book.Title,
				Author = book.Author,
				CategoryId = book.CategoryId,
				CategoryName = book.Category.Name // Safely access Name, will be null if Category is null
			})
			.ToListAsync();

		return books;
	}
	// GET: api/Books/5
	[HttpGet("{Isbn}")]
	public async Task<ActionResult<Book>> GetBook(string Isbn)
	{
		var book = await _context.Books.FindAsync(Isbn);

		if (book == null)
		{
			return NotFound();
		}

		return book;
	}

	// POST: api/Books
	[HttpPost]
	public async Task<ActionResult<Book>> PostBook(Book book)
	{
		_context.Books.Add(book);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetBook), new { Isbn = book.Isbn }, book);
	}

	// PUT: api/Books/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutBook(string Isbn, Book book)
	{
		if (Isbn != book.Isbn)
		{
			return BadRequest();
		}

		_context.Entry(book).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!_context.Books.Any(e => e.Isbn == Isbn))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}

	// DELETE: api/Books/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBook(int id)
	{
		var book = await _context.Books.FindAsync(id);
		if (book == null)
		{
			return NotFound();
		}

		_context.Books.Remove(book);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
