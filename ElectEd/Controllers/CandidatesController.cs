using ElectEd.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CandidatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            return await _context.Candidates
                                 .ToListAsync();
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _context.Candidates
                                       .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        // POST: api/Candidates
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(CandidateDto candidateDto)
        {
            int id = _context.Elections.Max(x => x.Id) + 1;
            var candidate = new Candidate
            {
                Id=id,
                Name = candidateDto.Name,
                Partylist = candidateDto.Partylist,
                Year = candidateDto.Year,
                Course = candidateDto.Course,
                ImagePath = candidateDto.ImagePath,
                ElectionId = candidateDto.ElectionId,
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
