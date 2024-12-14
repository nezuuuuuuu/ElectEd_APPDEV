using ElectEd.DTO;
using ElectEd.Services.Election;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace ElectEd.Services.Position
{
    public class PositionInfoService: IPositionInfoService
    {
        private readonly ApplicationDbContext _context; // Injected DbContext

        public PositionInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        PositionDtoWithId? IPositionInfoService.GetPositionById(int id)
        {
            var position = _context.Positions
                                     .FirstOrDefault(c => c.Id == id);


            if (position == null)
            {
                return null;
            }

            var positionDto = new PositionDtoWithId
            {
                Id = position.Id,
                Title = position.Title,
                ElectionId = position.ElectionId,
                MaxSelection = position.MaxSelection,



            };

            return positionDto;
        }

        Task<List<PositionDtoWithId>> IPositionInfoService.GetPositions()
        {
            var positions = _context.Positions
                .Select(position => new PositionDtoWithId
                {
                    Id = position.Id,
                    Title = position.Title,
                    ElectionId = position.ElectionId,
                    MaxSelection = position.MaxSelection,


                })
                .ToList();

            return Task.FromResult(positions);
        }
    }
}
