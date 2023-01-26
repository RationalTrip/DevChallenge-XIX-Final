using Miners.Application.Dtos.Images;
using Miners.Application.Dtos.Mines;
using Miners.Application.LogicUnits;
using Miners.Application.Services;
using Miners.Domain.Common;
using Miners.Domain.Exceptions.Results;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Miners.Infrastructure.Services
{
    public class MineScannerService : IMineScannerService
    {
        private readonly IMineScannerLogicUnit _mineScannerLogic;

        public MineScannerService(IMineScannerLogicUnit mineScannerLogic) => _mineScannerLogic = mineScannerLogic;
        public Result<List<MinesOutputDto>> ScanImageForMines(ImageInputDto imageInputDto)
        {
            Image<L8> image;
            try
            {
                image = ImageFromBase64(imageInputDto.Image);
            }
            catch (ImageFormatException exp)
            {
                return new(new UnprocessableImageException(exp.Message));
            }


            var cellSizeResult = _mineScannerLogic.FindSquireCellSize(image);
            if (cellSizeResult.IsFaulted)
                return cellSizeResult.AsErrorResult<List<MinesOutputDto>>();

            var cellSize = cellSizeResult.Value;

            return _mineScannerLogic.GetFilteredCellMines(image, cellSize, imageInputDto.MinLevel);
        }

        private Image<L8> ImageFromBase64(string imageBase64)
        {
            var imageBuffer = Convert.FromBase64String(imageBase64);

            return Image.Load<L8>(imageBuffer);
        }
    }
}
