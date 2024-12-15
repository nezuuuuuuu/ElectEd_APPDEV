using ElectEd.DTO;

namespace ElectEd.Services.VoteSlip
{
    public interface IVoteSlipInfoService
    {
        Task<List<VoteSlipDtoWithId>> GetVoteSlips();

        VoteSlipDtoWithId? GetVoteSlipById(int id);
    }
}
