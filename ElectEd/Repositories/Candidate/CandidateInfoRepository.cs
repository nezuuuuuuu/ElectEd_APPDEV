using ElectEd.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace ElectEd.Repositories.Candidate
{
    public class CandidateInfoRepository : ICandidateInfoRepository
    {
        private readonly ApplicationDbContext _context; // Injected DbContext

        public CandidateInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

         public IQueryable<Models.Candidate> GetCandidates()
        {
            return _context.Candidates;
        }
    }
}
