﻿using ElectEd.DTO;
using ElectEd.Services.Candidate;
using ElectEd.Services.Position;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ElectEd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPositionInfoService _positionInfoService;

   
        public PositionsController(ApplicationDbContext context, IPositionInfoService positionInfoService)
        {
            _context = context;
            _positionInfoService = positionInfoService;

        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            var position = _positionInfoService.GetPositions();

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            var position = _positionInfoService.GetPositionById(id);

            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        // POST: api/Positions
        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(PositionDto positionDto)
        {
            int id = _context.Positions.Any() ? _context.Positions.Max(x => x.Id) + 1 : 1;


            var election = _context.Elections.Find(positionDto.ElectionId);
            if (election==null)
            {
                return NotFound($"election with id {positionDto.ElectionId} not found");
            }
            var position = new Position
            {    

                Id = id,
                Title = positionDto.Title,
                Election= election,
                ElectionId = positionDto.ElectionId,
                MaxSelection = positionDto.MaxSelection,

            };


            // The Id will automatically be generated and incremented by EF Core
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            // Return the newly created election with its URL
            return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
         
        }

        [HttpGet("election/{electionId}")]
        public async Task<IActionResult> GetPositiobByElection(int electionId)
        {
            var position = await _positionInfoService.GetPositionsByElectionId(electionId);
            if (position == null || !position.Any())
            {
                return NotFound(new { Message = "No position found for this election." });
            }

            return Ok(position);
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

            var election = _context.Elections.Find(positionDto.ElectionId);
            if (election == null)
            {
                return NotFound($"election with id {positionDto.ElectionId} not found");
            }

            // Only update properties (no need to update the 'id' field)
            existingPosition.Title = positionDto.Title;
            existingPosition.ElectionId = positionDto.ElectionId;
            existingPosition.MaxSelection = positionDto.MaxSelection;
            existingPosition.Election = election;

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
