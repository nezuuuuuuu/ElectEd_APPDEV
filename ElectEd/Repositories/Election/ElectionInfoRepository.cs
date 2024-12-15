using ElectEd.DTO;
using ElectEd.Repositories.Election;
using static System.Collections.Specialized.BitVector32;

namespace ElectEd.Repositories.Election
{
    public class ElectionInfoRepository : IElectionInfoRepository
    {
        private readonly ApplicationDbContext _context; // Injected DbContext

        public ElectionInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public IQueryable<Models.Election> GetElections()
        {
            return _context.Elections;
        }
    }
}
