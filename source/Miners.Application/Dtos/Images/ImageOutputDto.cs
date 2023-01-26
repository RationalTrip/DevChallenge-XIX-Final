using Miners.Application.Dtos.Mines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miners.Application.Dtos.Images
{
    public record class ImageOutputDto
    {
        public List<MinesOutputDto> MyProperty { get; } = new List<MinesOutputDto>();
    }
}
