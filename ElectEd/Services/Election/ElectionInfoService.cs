using ElectEd.DTO;
using static System.Collections.Specialized.BitVector32;

namespace ElectEd.Services.Election
{
    public class ElectionInfoService: IElectionInfoService
    {
        private readonly ApplicationDbContext _context; // Injected DbContext

        public ElectionInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        ElectionDtoWithId? IElectionInfoService.GetElectionById(int id)
        {
            var election = _context.Elections
                                     .FirstOrDefault(c => c.Id == id);


            if (election == null)
            {
                return null;
            }

            var electionDto = new ElectionDtoWithId
            {
                Id = id,
                Title = election.Title,
                ImagePath = election.ImagePath,
                Description = election.Description,
                Departments = election.Departments,
                OpenDate = election.OpenDate,
                CloseDate = election.CloseDate

            };

            return electionDto;
        }

        Task<List<ElectionDtoWithId>> IElectionInfoService.GetElections()
        {
            var elections = _context.Elections
                .Select(election => new ElectionDtoWithId
                {
                    Id = election.Id,
                    Title = election.Title,
                    ImagePath = election.ImagePath,
                    Description = election.Description,
                    Departments = election.Departments,
                    OpenDate = election.OpenDate,
                    CloseDate = election.CloseDate

                })
                .ToList();

            return Task.FromResult(elections);
        }
    }
}
