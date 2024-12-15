using ElectEd.DTO;
using ElectEd.Repositories.Candidate;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace ElectEd.Services.Candidate
{
    public class CandidateInfoService : ICandidateInfoService
    {
        public readonly ICandidateInfoRepository _candidateInfoRepository;

        public CandidateInfoService(ICandidateInfoRepository context)
        {
            _candidateInfoRepository = context;
        } 
        public CandidateDtoWithId? GetCandidateById(int id)
        {
            var candidate = _candidateInfoRepository.GetCandidates()
        .Include(c => c.Election)  // Eagerly load Election
        .Include(c => c.Position)  // Eagerly load Position
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
                Election = candidate.Election,
                PositionId = candidate.PositionId,
                Position = candidate.Position,
                VoteCount = candidate.VoteCount,
                Platforms = candidate.Platforms,
                IsWinner = candidate.IsWinner,
               
                

            };

            return candidateDto;
        }

        public Task<List<CandidateDtoWithId>> GetCandidates()
        {
            var candidates = _candidateInfoRepository.GetCandidates()
               .Select(candidate => new CandidateDtoWithId
                {
                    Id = candidate.Id,
                    Name = candidate.Name,
                    Partylist = candidate.Partylist,
                    Year = candidate.Year,
                    Course = candidate.Course,
                    ImagePath = candidate.ImagePath,
                    ElectionId = candidate.ElectionId,
                    Election = candidate.Election,
                    Position = candidate.Position,
                    PositionId = candidate.PositionId,
                    VoteCount = candidate.VoteCount,
                    Platforms = candidate.Platforms,
                    IsWinner = candidate.IsWinner,

                })
                .ToList(); 

            return Task.FromResult(candidates);
        }

        public Task<List<CandidateDtoWithId>> GetCandidatesByElectionId(int electionId)
        {
            var candidates = _candidateInfoRepository.GetCandidates()
                .Where(candidate => candidate.ElectionId == electionId)
                .Select(candidate => new CandidateDtoWithId
                {
                    Id = candidate.Id,
                    Name = candidate.Name,
                    Partylist = candidate.Partylist,
                    Year = candidate.Year,
                    Course = candidate.Course,
                    ImagePath = candidate.ImagePath,
                    ElectionId = candidate.ElectionId,
                    Election = candidate.Election,
                    PositionId = candidate.PositionId,
                    Position = candidate.Position,
                    VoteCount = candidate.VoteCount,
                    Platforms = candidate.Platforms,
                    IsWinner = candidate.IsWinner,
                   
                    
                })
                .ToList();

            return Task.FromResult(candidates);
        }
    }
}
