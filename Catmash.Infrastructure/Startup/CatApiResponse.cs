using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Infrastructure.Startup
{
    public class CatApiResponse
    {
        public List<ExternalCatDto> Images { get; set; } = new();
    }
}
