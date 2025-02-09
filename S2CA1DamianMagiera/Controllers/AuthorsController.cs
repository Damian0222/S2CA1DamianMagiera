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

    [Route("api/authors")] //URL 
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // Represents the database context
        private readonly Library _context;

        // Constructor to inject the database context
        public AuthorsController(Library context)
        {
            _context = context;
        }

        [HttpGet] //GET requests to get all authors
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
                // Converts to a list and returns asynchronously
                .ToListAsync();
        }

        //GET requests to get a single author by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            // Searches for the author by ID
            var author = await _context.Authors.FindAsync(id);
            // Checks if author exists
            if (author == null)
            {
                // Returns error
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
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchAuthors(string name = "")
        {
            var authors = await _context.Authors
                //Makes sure books are included with authors
                .Include(a => a.Books)
                //Checks for authors whose names contain the search
                .Where(a => a.Name.Contains(name))
                .ToListAsync();
            //If no matching authors found
            if (!authors.Any())
            {
                 //Errors message
                return NotFound("No authors found.");
            }
            // Creates the response
            return authors.Select(a => new
            {
                a.Name,
                a.DateOfBirth,
                a.Nationality,
                a.Bio,
                //Formats the list of books
                Books = a.Books.Select(b => new
                {
                    b.Title,
                    b.YearPublished,
                    b.Genre,
                    b.PageCount
                //converts books to list
                }).ToList() 
                //Converts authors to a list and returns repsonse
            }).ToList();
        }

        //PUT requests to update an existing author by ID
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDTO authorDTO)
        {
            // Finds the author by ID
            var author = await _context.Authors.FindAsync(id);
            // Checks if author exists
            if (author == null)
            {
                // Returns 404 if not found
                return NotFound(); 
            }

            author.Name = authorDTO.Name; // Updates Name
            author.DateOfBirth = authorDTO.DateOfBirth;
            author.Nationality = authorDTO.Nationality; 
            author.Bio = authorDTO.Bio;
        
            // Saves changes to the database
            await _context.SaveChangesAsync();
            //Returns successful update
            return NoContent(); 
        }
        // Handles POST requests to create a new author
        [HttpPost] 
        public async Task<ActionResult<AuthorDTO>> CreateAuthor(AuthorDTO authorDTO)
        {
            var author = new Author
            {
                Name = authorDTO.Name, //Posts Name
                DateOfBirth = authorDTO.DateOfBirth, //Posts DateOfBirth
                Nationality = authorDTO.Nationality, //Posts Nationality
                Bio = authorDTO.Bio //Posts Bio
            };
            //Adds the new author to the database
            _context.Authors.Add(author);
            //Saves changes
            await _context.SaveChangesAsync();
            // Returns 201 Created response with the new author data
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, authorDTO); 
        }
        // Handles DELETE requests to remove an author by ID
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            // Finds the author by ID
            var author = await _context.Authors.FindAsync(id);
            // Checks if author exists
            if (author == null) 
            {
                // Returns 404 if not found
                return NotFound(); 
            }

            //Deletes the author from the database
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            // Returns successful deletion
            return NoContent(); 
        }
    }
}