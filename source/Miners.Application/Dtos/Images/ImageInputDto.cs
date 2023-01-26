using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Miners.Application.Dtos.Images
{
    public class ImageInputDto
    {
        [Required]
        [JsonPropertyName("min_level")]
        [Range(0, 100)]
        public int MinLevel { get; set; }

        [Required]
        [MinLength(1)]
        [RegularExpression(@"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$")]
        public string Image { get; set; } = string.Empty;
    }
}
