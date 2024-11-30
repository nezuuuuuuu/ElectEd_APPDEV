using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteSlipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VoteSlipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VoteSlips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoteSlip>>> GetVoteSlips()
        {
            return await _context.VoteSlips.ToListAsync();
        }

        // GET: api/VoteSlips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoteSlip>> GetVoteSlip(int id)
        {
            var voteSlip = await _context.VoteSlips.FindAsync(id);

            if (voteSlip == null)
            {
                return NotFound();
            }

            return voteSlip;
        }

        // PUT: api/VoteSlips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoteSlip(int id, VoteSlip voteSlip)
        {
            if (id != voteSlip.Id)
            {
                return BadRequest();
            }

            _context.Entry(voteSlip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteSlipExists(id))
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

        // POST: api/VoteSlips
        [HttpPost]
        public async Task<ActionResult<VoteSlip>> PostVoteSlip(VoteSlip voteSlip)
        {
            _context.VoteSlips.Add(voteSlip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoteSlip", new { id = voteSlip.Id }, voteSlip);
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
