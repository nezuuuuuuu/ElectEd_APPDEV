using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectEd.DTO;
using ElectEd.Services.Candidate;
using ElectEd.Services.Election;
namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionsController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IElectionInfoService _electionInfoService;

        public ElectionsController(ApplicationDbContext context, IElectionInfoService electionInfoService )
        {
            _context = context;
            _electionInfoService = electionInfoService;
        }

        // GET: api/Elections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Election>>> GetElections()
        {
            var election = _electionInfoService.GetElections();

            if (election == null)
            {
                return NotFound();
            }

            return Ok(election);
        }

        // GET: api/Elections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Election>> GetElection(int id)
        {
            var election = _electionInfoService.GetElectionById(id);

            if (election == null)
            {
                return NotFound();
            }

            return Ok(election);
        }

        // POST: api/Elections
        [HttpPost]
        public async Task<ActionResult<Election>> PostElection(ElectionDto electionDto)
        {
            int id = _context.Elections.Max(x => x.Id)+1;
            var election = new Election
            {
                Id = id,
                Title = electionDto.Title,
                ImagePath = electionDto.ImagePath,
                Description = electionDto.Description,
                Departments = electionDto.Departments ,
                OpenDate = electionDto.OpenDate,
                CloseDate = electionDto.CloseDate
            };

            // The Id will automatically be generated and incremented by EF Core
            _context.Elections.Add(election);
            await _context.SaveChangesAsync();

            // Return the newly created election with its URL
            return CreatedAtAction(nameof(GetElection), new { id = election.Id }, election);
        }

        // PUT: api/Elections/5
        [HttpPut]
        [Route("api/elections/{id}")]
        public async Task<IActionResult> PutElection(int id, ElectionDto electionDto)
        {
            // Fetch the existing election entity from the database
            var existingElection = await _context.Elections.SingleOrDefaultAsync(x => x.Id == id);
            if (existingElection == null)
            {
                return NotFound($"Election with id {id} does not exist.");
            }

            // Only update properties (no need to update the 'id' field)
            existingElection.Title = electionDto.Title;
            existingElection.ImagePath = electionDto.ImagePath;
            existingElection.Description = electionDto.Description;
            existingElection.Departments = electionDto.Departments;
            existingElection.OpenDate = electionDto.OpenDate;
            existingElection.CloseDate = electionDto.CloseDate;

            // Save changes to the database
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

            // Return the updated election
            return Ok(existingElection);
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
