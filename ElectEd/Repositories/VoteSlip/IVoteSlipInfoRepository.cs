using ElectEd.DTO;

namespace ElectEd.Repositories.VoteSlip
{
    public interface IVoteSlipInfoRepository
    {
        IQueryable<Models.VoteSlip> GetVoteSlip();

    }
}
