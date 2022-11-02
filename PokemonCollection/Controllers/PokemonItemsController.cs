using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonCollection.Models;

namespace PokemonCollection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonItemsController : ControllerBase
    {
        private readonly PokemonContext _context;

        public PokemonItemsController(PokemonContext context)
        {
            _context = context;
        }

        // GET: api/PokemonItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PokemonItem>>> GetPokemonItems()
        {
            return await _context.PokemonItems.ToListAsync();
        }

        // GET: api/PokemonItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PokemonItem>> GetPokemonItem(long id)
        {
            var pokemonItem = await _context.PokemonItems.FindAsync(id);

            if (pokemonItem == null)
            {
                return NotFound();
            }

            return pokemonItem;
        }

        // PUT: api/PokemonItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemonItem(long id, PokemonItem pokemonItem)
        {
            if (id != pokemonItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(pokemonItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonItemExists(id))
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

        // POST: api/PokemonItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PokemonItem>> PostPokemonItem(PokemonItem pokemonItem)
        {
            _context.PokemonItems.Add(pokemonItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPokemonItem", new { id = pokemonItem.Id }, pokemonItem);
            return CreatedAtAction(nameof(GetPokemonItem), new { id = pokemonItem.Id }, pokemonItem);
        }

        // DELETE: api/PokemonItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemonItem(long id)
        {
            var pokemonItem = await _context.PokemonItems.FindAsync(id);
            if (pokemonItem == null)
            {
                return NotFound();
            }

            _context.PokemonItems.Remove(pokemonItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PokemonItemExists(long id)
        {
            return _context.PokemonItems.Any(e => e.Id == id);
        }
    }
}
