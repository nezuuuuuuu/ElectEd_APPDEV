using ElectEd.DTO;

namespace ElectEd.Repositories.Candidate
{
    public interface ICandidateInfoRepository
    {
        IQueryable<Models.Candidate> GetCandidates(); // For deferred execution

    }
}
