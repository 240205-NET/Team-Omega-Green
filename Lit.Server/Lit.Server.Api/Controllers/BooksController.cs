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


	private readonly ILogger<BooksController> _logger;
	public BooksController(ILogger<BooksController> logger, ApplicationContext context, IConfiguration configuration)
	{
		_logger = logger;
		_context = context;
		_configuration = configuration;
	}

	// When querying and returning data from your controller
	[HttpGet]
	public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
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
	public async Task<ActionResult<BookDTO>> GetBookByISBN(string Isbn)
	{
		var book = await _context.Books
			.Include(b => b.Category)
			.Select(b => new BookDTO
			{
				Isbn = b.Isbn,
				Title = b.Title,
				Author = b.Author,
				CategoryId = b.CategoryId,
				CategoryName = b.Category.Name
			})
			.FirstOrDefaultAsync(b => b.Isbn == Isbn);

		if (book == null)
		{
			return NotFound();
		}

		return book;
	}


	// POST: api/Books
	[HttpPost]
	public async Task<ActionResult<Book>> CreateBook(BookDTO bookDto)
	{
		// Validate the CategoryId exists
		var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == bookDto.CategoryId);
		if (!categoryExists)
		{
			return BadRequest("Invalid CategoryId");
		}

		// Manually map BookDTO to Book entity
		Book book = new Book
		{
			Isbn = bookDto.Isbn,
			Title = bookDto.Title,
			Author = bookDto.Author,
			CategoryId = bookDto.CategoryId
			// Since Category is a navigation property, EF will handle it based on the CategoryId
		};

		// Add the book entity to the context and save changes
		_context.Books.Add(book);
		await _context.SaveChangesAsync();

		// Return the created book with the action used to get a book by ISBN
		// Ensure there's a GetBookByISBN method available in your controller to make this work
		return CreatedAtAction(nameof(GetBookByISBN), new { Isbn = book.Isbn }, book);
	}



	[HttpPut("{Isbn}")]
	public async Task<IActionResult> EditBook(string Isbn, [FromBody] BookDTO bookDto)
	{
		// Check model state at the beginning
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		if (Isbn != bookDto.Isbn)
		{
			return BadRequest("The ISBN in the URL does not match the ISBN in the body.");
		}

		var book = await _context.Books.FindAsync(Isbn);
		if (book == null)
		{
			return NotFound();
		}

		// Check if CategoryId exists
		var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == bookDto.CategoryId);
		if (!categoryExists)
		{
			return BadRequest("Invalid CategoryId.");
		}

		// Update book properties
		book.Title = bookDto.Title;
		book.Author = bookDto.Author;
		book.CategoryId = bookDto.CategoryId;
		// Since Category is a navigation property, EF will handle it based on the CategoryId

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
	[HttpDelete("{Isbn}")]
	public async Task<IActionResult> DeleteBook(string Isbn)
	{
		var book = await _context.Books.FindAsync(Isbn);
		if (book == null)
		{
			return NotFound();
		}

		_context.Books.Remove(book);
		await _context.SaveChangesAsync();

		return NoContent();
	}

}

public class BookDTO
{
	public string Isbn { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public int CategoryId { get; set; }
	// Optionally include category details if needed
	public string CategoryName { get; set; }
}