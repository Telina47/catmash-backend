using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Domain
{
    public class Cat
    {
        public string Id { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public double Score { get; set; } = 1000;
        public int Wins { get; set; }
        public int Losses { get; set; }
    }

}
