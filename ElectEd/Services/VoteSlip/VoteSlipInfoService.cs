using ElectEd.DTO;
using ElectEd.Repositories.Student;
using ElectEd.Repositories.VoteSlip;
using static System.Collections.Specialized.BitVector32;

namespace ElectEd.Services.VoteSlip
{
    public class VoteSlipInfoService : IVoteSlipInfoService
    {


        public readonly IVoteSlipInfoRepository _voteslipInfoRepository; // Injected DbContext

        public VoteSlipInfoService(IVoteSlipInfoRepository context)
        {
            _voteslipInfoRepository = context;
        }

        VoteSlipDtoWithId? IVoteSlipInfoService.GetVoteSlipById(int id)
        {
            var voteslip = _voteslipInfoRepository.GetVoteSlip()
                                    .FirstOrDefault(c => c.Id == id);

            if (voteslip == null)
            {
                return null;
            }
            var voteslipDto = new VoteSlipDtoWithId
            {
                Id = id,
                StudentId = voteslip.StudentId,
                ElectionId = voteslip.ElectionId,
                CandidateIds = voteslip.CandidateIds,
                Election = voteslip.Election


            };

            return voteslipDto;
        }

        Task<List<VoteSlipDtoWithId>> IVoteSlipInfoService.GetVoteSlips()
        {

            var voteslips = _voteslipInfoRepository.GetVoteSlip()
                .Select(voteslip => new VoteSlipDtoWithId
                {
                    Id = voteslip.Id,
                    StudentId = voteslip.StudentId,
                    ElectionId = voteslip.ElectionId,
                    CandidateIds = voteslip.CandidateIds,
                    Election = voteslip.Election

                })
                .ToList();

            return Task.FromResult(voteslips);
        }
    }
}
