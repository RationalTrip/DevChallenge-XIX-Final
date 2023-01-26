using Microsoft.AspNetCore.Mvc;
using Miners.Application.Dtos.Images;
using Miners.Application.Dtos.Mines;
using Miners.Application.Services;

namespace Miners.WebApi.Controllers
{
    [ApiController]
    [Route("/api/image-input")]
    public class MineScannerController : ControllerBase
    {
        private readonly IMineScannerService _scannerService;

        public MineScannerController(IMineScannerService scannerService) => _scannerService = scannerService;

        [HttpPost]
        public ActionResult<List<MinesOutputDto>> ScanImageForMines(ImageInputDto inputDto) =>
            _scannerService.ScanImageForMines(inputDto)
            .Match<ActionResult>(
                        message => Ok(message),
                        error => error.HandleError());
    }
}