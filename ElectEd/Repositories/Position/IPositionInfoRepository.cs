using ElectEd.DTO;

namespace ElectEd.Repositories.Position
{
    public interface IPositionInfoRepository
    {

        IQueryable<Models.Position> GetPositions(); 
    }
}
