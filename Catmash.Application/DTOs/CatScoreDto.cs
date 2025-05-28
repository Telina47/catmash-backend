using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Application.DTOs
{
    public class CatScoreDto
    {
        public string Id { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public double Score { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}
