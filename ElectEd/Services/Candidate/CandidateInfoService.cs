using ElectEd.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace ElectEd.Services.Candidate
{
    public class CandidateInfoService : ICandidateInfoService
    {
        private readonly ApplicationDbContext _context; // Injected DbContext

        public CandidateInfoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public CandidateDtoWithId? GetCandidateById(int id)
        {
            var candidate = _context.Candidates
                                     .FirstOrDefault(c => c.Id == id);
            

            if (candidate == null)
            {
                return null;
            }
           
            var candidateDto = new CandidateDtoWithId
            {
                Id = candidate.Id,

                Name = candidate.Name,
                Partylist = candidate.Partylist,
                Year = candidate.Year,
                Course = candidate.Course,
                ImagePath = candidate.ImagePath,
                ElectionId = candidate.ElectionId,
                PositionId = candidate.PositionId,
                VoteCount = candidate.VoteCount,
                Platforms = candidate.Platforms,
                IsWinner = candidate.IsWinner,

            };

            return candidateDto;
        }

        public Task<List<CandidateDtoWithId>> GetCandidates()
        {
            var candidates = _context.Candidates
                .Select(candidate => new CandidateDtoWithId
                {
                    Id = candidate.Id,
                    Name = candidate.Name,
                    Partylist = candidate.Partylist,
                    Year = candidate.Year,
                    Course = candidate.Course,
                    ImagePath = candidate.ImagePath,
                    ElectionId = candidate.ElectionId,
                    PositionId = candidate.PositionId,
                    VoteCount = candidate.VoteCount,
                    Platforms = candidate.Platforms,
                    IsWinner = candidate.IsWinner,

                })
                .ToList(); 

            return Task.FromResult(candidates);
        }
    }
}
