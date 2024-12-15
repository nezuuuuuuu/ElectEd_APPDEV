using ElectEd.DTO;
using ElectEd.Repositories.Candidate;
using ElectEd.Repositories.Election;
using static System.Collections.Specialized.BitVector32;

namespace ElectEd.Services.Election
{
    public class ElectionInfoService: IElectionInfoService
    {
        public readonly IElectionInfoRepository _electionInfoRepository; // Injected DbContext

        public ElectionInfoService(IElectionInfoRepository context)
        {
            _electionInfoRepository = context;
        }

        ElectionDtoWithId? IElectionInfoService.GetElectionById(int id)
        {
            var election = _electionInfoRepository.GetElections()
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
            var elections = _electionInfoRepository.GetElections()
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
