﻿using System;
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
        private readonly BookContext _context;

        public AuthorsController(BookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors.Select(a => new AuthorDTO
            {

                Name = a.Name,
                Bio = a.Bio
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return new AuthorDTO { Name = author.Name, Bio = author.Bio };
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

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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
        public async Task<ActionResult<AuthorDTO>> CreateAuthor(AuthorDTO authorDTO)
        {
            var author = new Author
            {
                Name = authorDTO.Name,
                Bio = authorDTO.Bio

            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, new AuthorDTO
            {

                Name = author.Name,
                Bio = author.Bio
            });
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

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}

