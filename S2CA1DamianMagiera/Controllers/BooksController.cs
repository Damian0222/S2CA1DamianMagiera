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
            var books = await _context.Books.ToListAsync();
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
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

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
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
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
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            var book = new Book
            {
                Title = bookDTO.Title,
                YearPublished = bookDTO.YearPublished,
                Genre = bookDTO.Genre,
                PageCount = bookDTO.PageCount,
                AuthorId = bookDTO.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

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
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
