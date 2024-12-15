using ElectEd.DTO;

namespace ElectEd.Services.Position
{
    public interface IPositionInfoService
    {
          
        Task<List<PositionDtoWithId>> GetPositions();

        PositionDtoWithId? GetPositionById(int id);
        Task<List<PositionDtoWithId>> GetPositionsByElectionId(int electionId);
    }
}
