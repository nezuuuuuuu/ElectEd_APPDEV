using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        public class ElectionDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime OpenDate { get; set; }
            public DateTime CloseDate { get; set; }
        }
        private readonly ApplicationDbContext _context;

        public ElectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Elections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Election>>> GetElections()
        {
            return await _context.Elections.ToListAsync();
        }

        // GET: api/Elections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Election>> GetElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);

            if (election == null)
            {
                return NotFound();
            }

            return election;
        }

        // POST: api/Elections
        [HttpPost]
        public async Task<ActionResult<Election>> PostElection(Election election)
        {


            // The Id will automatically be generated and incremented by EF Core
            _context.Elections.Add(election);
            await _context.SaveChangesAsync();

            // Return the newly created election with its URL
            return CreatedAtAction(nameof(GetElection), new { id = election.Id }, election);
        }

        // PUT: api/Elections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElection(int id, Election election)
        {
            if (id != election.Id)
            {
                return BadRequest();
            }

            _context.Entry(election).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Elections.Any(e => e.Id == id))
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

        // DELETE: api/Elections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Election>> DeleteElection(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election == null)
            {
                return NotFound();
            }

            _context.Elections.Remove(election);
            await _context.SaveChangesAsync();

            return election;
        }
    }
}
