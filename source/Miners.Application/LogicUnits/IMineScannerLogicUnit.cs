using Miners.Application.Dtos.Mines;
using Miners.Domain.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Miners.Application.LogicUnits
{
    public interface IMineScannerLogicUnit
    {
        Result<Size> FindSquireCellSize(Image<L8> image);
        List<MinesOutputDto> GetFilteredCellMines(Image<L8> image, Size cellSize, int minesMinChance);
    }
}
