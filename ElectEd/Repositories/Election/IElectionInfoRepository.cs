
namespace ElectEd.Repositories.Election
{
    public interface IElectionInfoRepository
    {
        IQueryable<Models.Election> GetElections(); // For deferred execution

    }
}
