using ElectEd.DTO;

namespace ElectEd.Services.Election
{
    public interface IElectionInfoService
    {
        Task<List<ElectionDtoWithId>> GetElections();

        ElectionDtoWithId? GetElectionById(int id);
    }
}
