using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S2CA1DamianMagiera.Data;
using S2CA1DamianMagiera.DTO;
using S2CA1DamianMagiera.Models;

namespace S2CA1DamianMagiera.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Library _context;

        public BooksController(Library context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            //Gets all books from the database
            var books = await _context.Books.ToListAsync();
            // Convert Book entities to BookDTO objects
            return books.Select(b => new BookDTO
            {
                Title = b.Title,
                YearPublished = b.YearPublished,
                Genre = b.Genre,
                PageCount = b.PageCount,
                AuthorId = b.AuthorId
            }).ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            // Find the book by ID
            var book = await _context.Books.FindAsync(id);
            // If book does not exist
            if (book == null)
            {
                // Return error
                return NotFound();
            }
            // Returns book details as a DTO
            return new BookDTO
            {
                Title = book.Title,
                YearPublished = book.YearPublished,
                Genre = book.Genre,
                PageCount = book.PageCount,
                AuthorId = book.AuthorId
            };
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDTO bookDTO)
        {
            // Find the book by ID
            var book = await _context.Books.FindAsync(id);
            // If book does not exist
            if (book == null)
            {
                // Return error
                return NotFound();
            }

            book.Title = bookDTO.Title;
            book.YearPublished = bookDTO.YearPublished;
            book.Genre = bookDTO.Genre;
            book.PageCount = bookDTO.PageCount;
            book.AuthorId = bookDTO.AuthorId;

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If book no longer exists
                if (!BookExists(id))
                {
                    // Return error
                    return NotFound();
                }
                else
                {
                    // Throws exception if different issue occurres
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            // Create a new book entity
            var book = new Book
            {
                Title = bookDTO.Title,
                YearPublished = bookDTO.YearPublished,
                Genre = bookDTO.Genre,
                PageCount = bookDTO.PageCount,
                AuthorId = bookDTO.AuthorId
            };
            // Add book to the database
            _context.Books.Add(book);
            // Saves changes
            await _context.SaveChangesAsync();
            // Returns the created book
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, new BookDTO
            {
                Title = book.Title,
                YearPublished = book.YearPublished,
                Genre = book.Genre,
                PageCount = book.PageCount,
                AuthorId = book.AuthorId
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            //Find the book by ID
            var book = await _context.Books.FindAsync(id);
            //If book does not exist
            if (book == null)
            {
                //Error
                return NotFound();
            }
            // Removes book from the database
            _context.Books.Remove(book);
            // Save 
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            // Check if book with given ID exists
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
