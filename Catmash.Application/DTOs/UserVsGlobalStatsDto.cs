using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Application.DTOs
{
    public class UserVsGlobalStatsDto
    {
        public List<CatScoreDto> TopGlobal { get; set; } = new();
        public List<CatScoreDto> TopUser { get; set; } = new();
        public List<CatScoreDto> PopularButYouDisagree { get; set; } = new();
        public List<CatScoreDto> UnderratedByOthers { get; set; } = new();
        public double AgreementRate { get; set; }
        public TopCount TopCount { get; set; } = TopCount.Top10;
    }
}
