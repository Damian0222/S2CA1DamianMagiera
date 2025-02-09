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

    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookstoreContext _context;

        public AuthorsController(BookstoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            return await _context.Authors
                .Select(a => new AuthorDTO
                {
                    Name = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    Nationality = a.Nationality,
                    Bio = a.Bio
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return new AuthorDTO
            {
                Name = author.Name,
                DateOfBirth = author.DateOfBirth,
                Nationality = author.Nationality,
                Bio = author.Bio
            };
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDTO authorDTO)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            author.Name = authorDTO.Name;
            author.DateOfBirth = authorDTO.DateOfBirth;
            author.Nationality = authorDTO.Nationality;
            author.Bio = authorDTO.Bio;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor(AuthorDTO authorDTO)
        {
            var author = new Author
            {
                Name = authorDTO.Name,
                DateOfBirth = authorDTO.DateOfBirth,
                Nationality = authorDTO.Nationality,
                Bio = authorDTO.Bio
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, authorDTO);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}