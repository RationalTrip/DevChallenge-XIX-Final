using Miners.Application.Dtos.Images;
using Miners.Application.Dtos.Mines;
using Miners.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miners.Application.Services
{
    public interface IMineScannerService
    {
        Result<List<MinesOutputDto>> ScanImageForMines(ImageInputDto imageInputDto);
    }
}
