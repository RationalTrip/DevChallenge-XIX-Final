using Miners.Application.Dtos.Mines;
using Miners.Application.LogicUnits;
using Miners.Domain.Common;
using Miners.Domain.Exceptions.Results;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Miners.Infrastructure.LogicUnits
{
    public class MineScannerLogicUnit : IMineScannerLogicUnit
    {
        public Result<Size> FindSquireCellSize(Image<L8> image)
        {
            Size cellSize = new Size(1, 1);

            while (TryGetNextPossibleSquireCell(image.Size(), cellSize, out cellSize))
                if (IsCellBordersOnlyWhite(image, cellSize))
                    return cellSize;

            return new(new CellsCannotBeDetectedException());
        }

        public List<MinesOutputDto> GetFilteredCellMines(Image<L8> image, Size cellSize, int minesMinChance)
        {
            List<MinesOutputDto> result = new List<MinesOutputDto>();

            for (int cellX = 0, cellXPosition = 1;
                cellXPosition < image.Width - cellSize.Width;
                cellX++, cellXPosition += cellSize.Width + 1)
            {
                for (int cellY = 0, cellYPosition = 1;
                    cellYPosition < image.Height - cellSize.Height;
                    cellY++, cellYPosition += cellSize.Height + 1)
                {
                    int minesChance = CalculateMinesChance(image, cellXPosition, cellYPosition, cellSize);

                    if (minesChance > minesMinChance)
                        result.Add(new MinesOutputDto(cellX, cellY, minesChance));
                }
            }

            return result;
        }

        private int CalculateMinesChance(Image<L8> image, int x, int y, Size cellSize)
        {
            int minesCount = 0;

            for (int positionX = x; positionX < x + cellSize.Width; positionX++)
            {
                for (int positionY = y; positionY < y + cellSize.Height; positionY++)
                {
                    if (IsBlack(image[positionX, positionY].PackedValue))
                        minesCount++;
                }
            }

            int totalSize = cellSize.Width * cellSize.Height;

            return minesCount * 100 / totalSize;
        }

        private bool IsCellBordersOnlyWhite(Image<L8> image, Size cellSize)
        {
            for (int borderX = 0; borderX < image.Width; borderX += cellSize.Width + 1)
            {
                for (int borderY = 0; borderY < image.Height; borderY++)
                    if (IsBlack(image[borderX, borderY].PackedValue))
                        return false;
            }

            for (int borderY = 0; borderY < image.Height; borderY += cellSize.Height + 1)
            {
                for (int borderX = 0; borderX < image.Width; borderX++)
                    if (IsBlack(image[borderX, borderY].PackedValue))
                        return false;
            }

            return true;
        }

        private bool TryGetNextPossibleSquireCell(Size imageSize, Size previousSize, out Size cellSize)
        {
            var currentSize = new Size(previousSize.Width + 1, previousSize.Height + 1);

            while (currentSize.Width < imageSize.Width && currentSize.Height < imageSize.Height)
            {
                if (IsSizeFit(imageSize, currentSize))
                {
                    cellSize = currentSize;
                    return true;
                }

                currentSize = new Size(currentSize.Width + 1, currentSize.Height + 1);
            }

            cellSize = new();
            return false;

            bool IsSizeFit(Size imageSize, Size cellSize) =>
                IsSideSizeFit(imageSize.Height, cellSize.Height) && IsSideSizeFit(imageSize.Width, cellSize.Width);

            bool IsSideSizeFit(int sideSize, int cellSideSize)
            {
                int cellCount = sideSize / cellSideSize;
                int borderSize = 1;
                int cellWithBorderSize;

                do
                {
                    cellWithBorderSize = cellCount * cellSideSize + borderSize * (cellCount + 1);

                    if (sideSize == cellWithBorderSize)
                        return true;

                    cellCount--;

                } while (cellWithBorderSize > sideSize);

                return false;
            }
        }

        private bool IsWhite(byte color) => color > 128;
        private bool IsBlack(byte color) => !IsWhite(color);
    }
}
