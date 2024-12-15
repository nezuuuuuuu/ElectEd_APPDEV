using ElectEd.DTO;
using ElectEd.Services.Student;
using ElectEd.Services.VoteSlip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteSlipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IVoteSlipInfoService _voteSlipInfoService;


        public VoteSlipsController(ApplicationDbContext context, IVoteSlipInfoService voteSlipInfoService)
        {
            _context = context;
            _voteSlipInfoService = voteSlipInfoService;
        }

        // GET: api/VoteSlips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoteSlip>>> GetVoteSlips()
        {
            var voteSlip = _voteSlipInfoService.GetVoteSlips();

            if (voteSlip == null)
            {
                return NotFound();
            }

            return Ok(voteSlip);
        }

        // GET: api/VoteSlips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoteSlip>> GetVoteSlip(int id)
        {
            var voteSlip = _voteSlipInfoService.GetVoteSlipById(id);

            if (voteSlip == null)
            {
                return NotFound();
            }

            return Ok(voteSlip);
        }

        // PUT: api/VoteSlips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoteSlip(int id, VoteSlipDto voteSlipDto)
        {
          

            var existingVoteSlip = await _context.VoteSlips.SingleOrDefaultAsync(x => x.Id == id);
            if (existingVoteSlip == null)
            {
                return NotFound($"Election with id {id} does not exist.");
            }

            var election = _context.Elections.Find(voteSlipDto.ElectionId);
            if (election == null)
            {
                return NotFound($"election with id {voteSlipDto.ElectionId} not found");
            }

            // Only update properties (no need to update the 'id' field)
            existingVoteSlip.StudentId = voteSlipDto.StudentId;
            existingVoteSlip.ElectionId = voteSlipDto.ElectionId;
            existingVoteSlip.CandidateIds = voteSlipDto.CandidateIds;
            existingVoteSlip.Election = election;


            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.VoteSlips.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return the updated election
            return Ok(existingVoteSlip);
        }

        // POST: api/VoteSlips
        [HttpPost]
        public async Task<ActionResult<VoteSlip>> PostVoteSlip(VoteSlipDto voteSlipDto)
        {

            int id = _context.VoteSlips.Any() ? _context.VoteSlips.Max(x => x.Id) + 1 : 1;

    
            var election = _context.Elections.Find(voteSlipDto.ElectionId);
            if (election == null)
            {
                return NotFound($"election with id {voteSlipDto.ElectionId} not found");
            }

            var voteSlip  = new VoteSlip
            {
                Id = id,
                StudentId= voteSlipDto.StudentId,
                ElectionId= voteSlipDto.ElectionId,
                CandidateIds= voteSlipDto.CandidateIds,
                Election = election


            };


                                     // The Id will automatically be generated and incremented by EF Core
            _context.VoteSlips.Add(voteSlip);
            await _context.SaveChangesAsync();

            // Return the newly created election with its URL
            return CreatedAtAction(nameof(GetVoteSlip), new { id = voteSlip.Id }, voteSlip);



        }

        // DELETE: api/VoteSlips/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VoteSlip>> DeleteVoteSlip(int id)
        {
            var voteSlip = await _context.VoteSlips.FindAsync(id);
            if (voteSlip == null)
            {
                return NotFound();
            }

            _context.VoteSlips.Remove(voteSlip);
            await _context.SaveChangesAsync();

            return voteSlip;
        }

        private bool VoteSlipExists(int id)
        {
            return _context.VoteSlips.Any(e => e.Id == id);
        }
    }
}
