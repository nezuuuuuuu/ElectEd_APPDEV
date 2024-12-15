using ElectEd.DTO;
using ElectEd.Services.Candidate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

using Models;
namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICandidateInfoService _candidateInfoService;
        
        public CandidatesController(ApplicationDbContext context, ICandidateInfoService candidateInfoService)
        {
            _context = context;

            _candidateInfoService = candidateInfoService;
           
        }

        // GET: api/Candidates
        [HttpGet]
        public  IActionResult GetCandidates()
        {
            var candidates= _candidateInfoService.GetCandidates();
            if (candidates == null)
            {
                return NotFound($"no candidates");
            }
            return Ok(candidates);
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public IActionResult GetCandidate(int id)
        {
            var candidates = _candidateInfoService.GetCandidateById(id);
            if (candidates == null)
            {
                return NotFound($"no candidates with id {id}");
            }
            return Ok(candidates);
        }

        // POST: api/Candidates
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(CandidateDto candidateDto)
        {
          

            int id = _context.Candidates.Any() ? _context.Candidates.Max(x => x.Id) + 1 : 1;


            var election = _context.Elections.Find(candidateDto.ElectionId);
            var position = _context.Positions.Find(candidateDto.PositionId);
    
            if(election==null || position ==null)
            {
                return NotFound($"Foreign key not found");
            }
            var candidate = new Candidate
            {
                Id=id,
                Name = candidateDto.Name,
                Partylist = candidateDto.Partylist,
                Year = candidateDto.Year,
                Course = candidateDto.Course,
                ImagePath = candidateDto.ImagePath,
                ElectionId = candidateDto.ElectionId,
                Election = election,
                Position = position,
               PositionId = candidateDto.PositionId,
                VoteCount = candidateDto.VoteCount,
                Platforms = candidateDto.Platforms,
               IsWinner = candidateDto.IsWinner
            };

            // The Id will automatically be generated and incremented by EF Core
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            // Return the newly created election with its URL
            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, candidate);
        }

        // PUT: api/Candidates/5
        [HttpPut]
        [Route("api/candidates/{id}")]
        public async Task<IActionResult> PutCandidate(int id, CandidateDto candidateDto)
        { // Fetch the existing election entity from the database
            var existingCandidate = await _context.Candidates.SingleOrDefaultAsync(x => x.Id == id);
            if (existingCandidate == null)
            {
                return NotFound($"Election with id {id} does not exist.");
            }

            // Only update properties (no need to update the 'id' field)
            existingCandidate.Name = candidateDto.Name;
            existingCandidate.Partylist = candidateDto.Partylist; 
            existingCandidate.Year = candidateDto.Year;
            existingCandidate.Course = candidateDto.Course;
            existingCandidate.ImagePath = candidateDto.ImagePath;
            existingCandidate.ElectionId = candidateDto.ElectionId;
            existingCandidate.PositionId = candidateDto.PositionId;
            existingCandidate.VoteCount = candidateDto.VoteCount;
            existingCandidate.Platforms = candidateDto.Platforms;
            existingCandidate.IsWinner = candidateDto.IsWinner;


            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Candidates.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return the updated election
            return Ok(existingCandidate);
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Candidate>> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return candidate;
        }
    }
}
