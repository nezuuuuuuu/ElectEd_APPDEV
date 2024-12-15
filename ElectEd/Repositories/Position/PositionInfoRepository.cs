using ElectEd.DTO;
using ElectEd.Repositories.Position;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace ElectEd.Repositories.Position
{
    public class PositionInfoRepository: IPositionInfoRepository
    {
        private readonly ApplicationDbContext _context; // Injected DbContext

        public PositionInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
  


            IQueryable<Models.Position> IPositionInfoRepository.GetPositions()
            {
                return _context.Positions;
            }
        }
    }

