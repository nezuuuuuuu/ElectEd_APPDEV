using ElectEd.DTO;
using static System.Collections.Specialized.BitVector32;

namespace ElectEd.Repositories.VoteSlip
{
    public class VoteSlipInfoRepository : IVoteSlipInfoRepository
    {



        private readonly ApplicationDbContext _context; // Injected DbContext

        public VoteSlipInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Models.VoteSlip> GetVoteSlip()
        {
            return _context.VoteSlips;
        }

    }
}
