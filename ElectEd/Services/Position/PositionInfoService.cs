using ElectEd.DTO;
using ElectEd.Repositories.Election;
using ElectEd.Repositories.Position;
using ElectEd.Services.Election;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace ElectEd.Services.Position
{
    public class PositionInfoService: IPositionInfoService
    {
        public readonly IPositionInfoRepository _positionInfoRepository; // Injected DbContext

        public PositionInfoService(IPositionInfoRepository context)
        {
            _positionInfoRepository = context;
        }

        PositionDtoWithId? IPositionInfoService.GetPositionById(int id)
        {
            var position = _positionInfoRepository.GetPositions()
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
            var positions = _positionInfoRepository.GetPositions()
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
