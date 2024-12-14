using ElectEd.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            return await _context.Positions
                                
                                 .ToListAsync();
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            var position = await _context.Positions
                                        
                                          .FirstOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }

        // POST: api/Positions
        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(PositionDto positionDto)
        {

            int id = _context.Positions.Max(x => x.Id) + 1;
            var position = new Position
            {    

                Id = id,
                Title = positionDto.Title,
                ElectionId = positionDto.ElectionId,
                MaxSelection = positionDto.MaxSelection,

            };

            // The Id will automatically be generated and incremented by EF Core
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            // Return the newly created election with its URL
            return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
         
        }

        // PUT: api/Positions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, PositionDto positionDto)
        {
         
            var existingPosition = await _context.Positions.SingleOrDefaultAsync(x => x.Id == id);
            if (existingPosition == null)
            {
                return NotFound($"Position with id {id} does not exist.");
            }

            // Only update properties (no need to update the 'id' field)
            existingPosition.Title = positionDto.Title;
            existingPosition.ElectionId = positionDto.ElectionId;
            existingPosition.MaxSelection = positionDto.MaxSelection;


            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Positions.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return the updated election
            return Ok(existingPosition);
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Position>> DeletePosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return position;
        }
    }
}
