using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Domain
{
    public class Vote
    {
        public Guid Id { get; set; }
        public string VoterId { get; set; } = null!;
        public string WinnerCatId { get; set; } = null!;
        public string LoserCatId { get; set; } = null!;
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;
    }

}
